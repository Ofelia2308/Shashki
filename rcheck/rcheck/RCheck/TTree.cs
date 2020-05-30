using System.Collections;
using System.Linq;

namespace RCheck
{
    class TTree
    {
        TNode Root;
        TPole Pole;

        public ArrayList Lists;

        public int Count;

        public TTree(TNode Root, TPole Pole)
        {
            this.Root = Root;
            this.Pole = Pole;

            Lists = new ArrayList(12);

            Count = 0;
        }

        public ArrayList Up(TNode List)
        {
            ArrayList Res = new ArrayList(6);

            TNode Cur = List;

            Res.Add(Cur);

            while (Cur.Parent != null)
            {
                Cur = Cur.Parent;
                Res.Add(Cur);
            }

            return Res;
        }

        public void AddRuns(TCheck Check, ref TRuns Runs)
        {
            for (int i = 0; i < Lists.Count; i++)
            {
                TNode Node = (TNode)Lists[i];

                TRun Run = new TRun(Check, Node.Pos);

                ArrayList arr = Up(Node);

                for (int j = 0; j < arr.Count; j++)
                {
                    Run.Killed.Add(((TNode)arr[j]).Killed);
                }

                Runs.Add(Run);

                TCell[] Addition = Node.Pos.NearsDama(Node.dir, Pole);

                for (int k = 1; k < Addition.Count(); k++)
                {
                    TRun ARun = new TRun(Check, Addition[k]);

                    for (int j = 0; j < Run.Killed.Count; j++)
                    {
                        ARun.Killed.Add(Run.Killed[j]);
                    }

                    Runs.Add(ARun);
                }
            }

        }
    }

    class TNode
    {
        public TNode Parent;
        public TCell Pos;
        public TCheck Killed;
        public int dir;

        public TNode(TNode Parent, TCell Pos, TCheck Killed, int dir)
        {
            this.Parent = Parent;
            this.Pos = Pos;
            this.Killed = Killed;
            this.dir = dir;
        }
    }
}
