namespace AdventOfCode.Puzzles;

public class Puzzle23 : Puzzle<(string A, string B), string>
{
    private const int PuzzleId = 23;

    public Puzzle23() : base(PuzzleId) { }

    public Puzzle23(params IEnumerable<(string A, string B)> inputEntries) : base(PuzzleId, inputEntries) { }

    private readonly Dictionary<string, HashSet<string>> _graph = [];
 
    public override string SolvePart1()
    {
        PopulateGraph();
        var connectedComputers = GetConnectedComputers();
        return connectedComputers.Count.ToString();
    }

    private HashSet<string> GetConnectedComputers()
    {
        HashSet<string> connectedComputers = [];
        
        foreach (var a in _graph.Keys)
        {
            // Get all the nodes A are connected to (we call each of them for B)
            var connectedToA = _graph[a];
            foreach (var b in connectedToA)
            {
                // Get all nodes (except A) B are connected to (we call each of them for C)
                var connectedToB = _graph[b].Where(ctb => ctb != a);
                foreach (var c in connectedToB)
                {
                    // If C (which is connected to B, which is connected to A)
                    // also is connected to A, we have a network where both
                    // A, B and C are connected to each other
                    var connectedToC = _graph[c];
                    if (connectedToC.Contains(a))
                    {
                        var nodes = new List<string> { a, b, c };
                        if (!nodes.Any(n => n.StartsWith('t')))
                        {
                            // The Chief Historian's computer is not one of A, B or C
                            continue;
                        }

                        nodes = nodes
                            .OrderBy(n => n) // To have a consistent order (which will remove duplicates in a HashSet)
                            .ToList();
                        var network = $"{nodes[0]}-{nodes[1]}-{nodes[2]}";
                        connectedComputers.Add(network);
                    }
                }
            }
        }
        
        return connectedComputers;
    }

    private void PopulateGraph()
    {
        foreach (var edge in InputEntries)
        {
            if (!_graph.TryGetValue(edge.A, out var connectedNodes))
            {
                connectedNodes = [];
                _graph[edge.A] = connectedNodes;
            }
            connectedNodes.Add(edge.B);
            
            if (!_graph.TryGetValue(edge.B, out connectedNodes))
            {
                connectedNodes = [];
                _graph[edge.B] = connectedNodes;
            }
            connectedNodes.Add(edge.A);
        }
    }
    
    private List<string> _longestNetwork = [];

    public override string SolvePart2()
    {
        ThrowHardCodedResult("?", "Too slow, haven't found answer", notFromTest: true);
        
        PopulateGraph();
        var chcCandidates = _graph.Keys.Where(k => k.StartsWith('t')); // Candidates for Chief Historian's computer
        foreach (var chcCandidate in chcCandidates)
        {
            BuildNetwork(chcCandidate, new HashSetTree<string>(string.Empty));
        }

        var password = string.Join(",", _longestNetwork.OrderBy(s => s));
        return password;
    }

    private void BuildNetwork(string computer, HashSetTree<string> network)
    {
        var networkNode = new HashSetTree<string>(computer);
        network.Add(networkNode);
        var newNetwork = networkNode
            .GetParents(includeSelf: true)
            .ToList()[..^1] // Leave out the root node with string.Empty
            .ToHashSet();

        // Exclude all the computers that are already in the graph
        var nextCandidatesAll = _graph
            .Where(node => !newNetwork.Contains(node.Key))
            .Select(kvp => (Candidate: kvp.Key, ConnectedTo: kvp.Value));
        // Only include the computers that are linked to all the existing computers in the network
        var nextCandidates = nextCandidatesAll
            // .Where(node => node.ConnectedTo.All(n => newNetwork.Contains(n)))
            .Where(node => newNetwork.All(n => node.ConnectedTo.Contains(n)))
            .ToList();
        if (nextCandidates.Count > 0)
        {
            foreach (var nextCandidate in nextCandidates)
            {
                BuildNetwork(nextCandidate.Candidate, networkNode);
            }
        }
        else if (newNetwork.Count > _longestNetwork.Count)
        {
            _longestNetwork = newNetwork.ToList();
        }
    }

    protected internal override (string A, string B) ParseInput(string inputItem)
    {
        var nodes = inputItem.Split('-');
        return (A: nodes[0], B: nodes[1]);
    }
}
