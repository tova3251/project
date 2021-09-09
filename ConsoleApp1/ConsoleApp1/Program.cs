using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            public class Dijkstra
        {
            myproject1Entities ent = new myproject1Entities();
            private List<NodeDto> _nodes;
            private List<EdgesDto> _edges;
            private List<NodeDto> _basis;
            private Dictionary<string, decimal> _dist;
            private Dictionary<string, NodeDto> _previous;

            //מקבלת לי את כל הצמתים וכל הקשתות
            public Dijkstra(List<EdgesDto> edge, List<NodeDto> nodes)
            {
                Edges = edge;
                Nodes = nodes;
                Basis = new List<NodeDto>();
                Dist = new Dictionary<string, decimal>();
                Previous = new Dictionary<string, NodeDto>();

                // record NodeDto 
                foreach (NodeDto n in Nodes)
                {
                    Previous.Add(n.IdNode.ToString(), null);//מכניס את כל הID למילון
                    Basis.Add(n);//מוסיף לבסיס את כל הצמתים
                    Dist.Add(n.IdNode.ToString(), decimal.MaxValue);//שם לכולם את המרחק הכי גדול
                }

            }

            /// Calculates the shortest path from the start
            ///  to all other nodes
            ///  
            //בודקת את המרחק הקצר מצומת ההתחלה לכל שאר הצמתים
            public void calculateDistance(NodeDto start)
            {
                Dist[start.IdNode.ToString()] = 0;//השמה למקום של צומת ההתחלה מרחק 0

                while (Basis.Count > 0)
                {
                    NodeDto u = getNodeWithSmallestDistance();//בפעם הראשונה בוחר את הצומת הראשון מכיון שהתחלנו את הדיסט שלו ל0
                    if (u == null)
                    {
                        Basis.Clear();
                    }
                    else
                    {
                        foreach (NodeDto v in getNeighbors(u))
                        {
                            decimal alt = Dist[u.IdNode.ToString()] + getDistanceBetween(u, v);
                            if (alt < Dist[v.IdNode.ToString()])
                            {
                                Dist[v.IdNode.ToString()] = alt;
                                Previous[v.IdNode.ToString()] = u;
                            }
                        }
                        Basis.Remove(u);
                    }
                }
            }

            public List<NodeDto> getPathTo(NodeDto d)//מקבלת את הצמתים הסופיים של הדרך
            {
                List<NodeDto> path = new List<NodeDto>();

                path.Insert(0, d);

                while (Previous[d.IdNode.ToString()] != null)
                {
                    d = Previous[d.IdNode.ToString()];
                    path.Insert(0, d);
                }

                return path;
            }

            public NodeDto getNodeWithSmallestDistance()//קבלת הצומת בעלת המשקל הנמוך ביותר ממה שנשאר בבסיס
            {
                decimal distance = decimal.MaxValue;
                NodeDto smallest = null;

                foreach (NodeDto n in Basis)
                {
                    if (Dist[n.IdNode.ToString()] < distance)
                    {
                        distance = Dist[n.IdNode.ToString()];
                        smallest = n;
                    }
                }

                return smallest;
            }


            public List<NodeDto> getNeighbors(NodeDto n)//מחזירה רשימה עם השכנים של הצומת שהתקבל
            {
                List<NodeDto> neighbors = new List<NodeDto>();

                foreach (EdgesDto e in Edges)
                {
                    if (e.IdNodeA.Equals(n.IdNode) && Basis.Contains(n))
                    {
                        var node1 = ent.Node.Where(x => x.IdNode == e.IdNodeB).First();
                        NodeDto node = NodeDto.DalToDto(node1);
                        neighbors.Add(node);
                    }
                }

                return neighbors;
            }

            public decimal getDistanceBetween(NodeDto o, NodeDto d)//פונקציה המחזירה מרחק בין שני קודקודים
            {
                foreach (EdgesDto e in Edges)
                {
                    if (e.IdNodeA.Equals(o.IdNode) && e.IdNodeB.Equals(d.IdNode))
                    {
                        return e.Lengths;
                    }
                }

                return 0;
            }


            public List<NodeDto> Nodes
            {
                get { return _nodes; }
                set { _nodes = value; }
            }

            public List<EdgesDto> Edges
            {
                get { return _edges; }
                set { _edges = value; }
            }

            public List<NodeDto> Basis
            {
                get { return _basis; }
                set { _basis = value; }
            }

            public Dictionary<string, decimal> Dist
            {
                get { return _dist; }
                set { _dist = value; }
            }

            public Dictionary<string, NodeDto> Previous
            {
                get { return _previous; }
                set { _previous = value; }
            }
        }
    }

}
    }

    // This code is contributed by ChitraNayal
}
  