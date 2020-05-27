using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;


namespace RCheck
{
    
    class TPole
    {
        Canvas g;

        public TChecks CW, CB;

        public TPole(Canvas g)
        {
            this.g = g;

            DrawCell();

            CW = new TChecks(WB.W, g);
            CB = new TChecks(WB.B, g);

            DrawCheckers();
        }

        public TCell GetCell(Point XY)
        {
            double L = g.Width; // сохранить размер игрового поля
            double L8 = L / 8.0;

            double x = XY.X;
            double y = XY.Y;

            if ((x < 0) || (x > L) || (y < 0) || (y > L))
            {
                return null;
            }

            int a = Convert.ToInt16(Math.Ceiling(x / L8));

            TNote Z = (TNote)(a - 1);

            a = Convert.ToInt16(Math.Ceiling(y / L8));
            int n = 9 - a;

            return new TCell(Z, n);
        }

        public void GameOver(WB C)
        {
            string s = "";

            if (C == WB.W)
            {
                s = "Черные";
            }
            else
            {
                s = "Белые";
            }

            MessageBox.Show(s + " победили!");
        }

        void DrawCheckers()
        {
            for (int k = 0; k < CW.Count; k++)
            {
                CW[k].O[0] = DrawCheck(CW[k].Pos, WB.W);
            }

            for (int k = 0; k < CB.Count; k++)
            {
                CB[k].O[0] = DrawCheck(CB[k].Pos, WB.B);
            }
        }

        Ellipse DrawCheck(TCell Pos, WB C)
        {
            double L = g.Width; // сохранить размер игрового поля
            double L8 = L / 8.0;
            double kappa = 0.8;

            Ellipse O = new Ellipse();

            if(C == WB.W)
            {
                O.Fill = Brushes.White;
            }
            else
            {
                O.Fill = Brushes.Black;
            }
            O.Width = kappa * L8;
            O.Height = kappa * L8;

            O.Margin = new Thickness(Pos.i * L8 + L8 * (1 - kappa) / 2, Pos.j * L8 + L8 * (1 - kappa) / 2, 0, 0);
            g.Children.Add(O);

            return O;
        }

        // нарисовать поле
        void DrawCell()
        {
            double L = g.Width; // сохранить размер игрового поля
            double L8 = L / 8.0;

            bool White = false;

            for (int i = 0; i < 8; i++)
            {
                White = !White;

                for (int j = 0; j < 8; j++)
                {
                    Rectangle Cell = new Rectangle();

                    if (White)
                    {
                        Cell.Fill = Brushes.Lavender;
                        White = false;
                    }
                    else
                    {
                        Cell.Fill = Brushes.Peru;
                        White = true;
                    }

                    Cell.Width = L8;
                    Cell.Height = L8;
                    Cell.Margin = new Thickness(i * L8, j * L8, 0, 0);
                    g.Children.Add(Cell);
                }
            }

            Rectangle R = new Rectangle();
            R.Width = L;
            R.Height = L;
            R.Stroke = Brushes.Black;
            R.Margin = new Thickness(0);
            g.Children.Add(R);

        }

        public bool Who(TCell Pos, out TCheck who)
        {
            who = null;

            if (Pos == null)
            {
                return false;
            }

            for (int k = 0; k < CW.Count; k++)
            {
                if (CW[k].Pos.Eq(Pos))
                {
                    who = CW[k];
                    return true;
                }
            }

            for (int k = 0; k < CB.Count; k++)
            {
                if (CB[k].Pos.Eq(Pos))
                {
                    who = CB[k];
                    return true;
                }
            }

            return true;
        }
        
    }

     enum TNote { A, B, C, D, E, F, G, H };

    class TCell
    {
        public int i, j; // координаты клетки на поле
        public TNote B; // буква
        public int n;

        public TCell(int i, int j)
        {
            this.i = i;
            this.j = j;

            n = i + 1;
            B = (TNote)j;
        }

        public TCell(TNote B, int n)
        {
            this.B = B;
            this.n = n;

            i = (int)B;

            j = 8 - n;
        }

        public void Set(TCell Pos)
        {
            this.i = Pos.i;
            this.j = Pos.j;
            this.B = Pos.B;
            this.n = Pos.n;
        }

        public bool Eq(TCell Pos)
        {
            if ((B == Pos.B) && (n == Pos.n))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public TCell[] NearsDama(int dir, TPole Pole)
        {
            ArrayList arr = new ArrayList(8);

            TNote B_ = B;
            int n_ = n;

            arr.Add(new TCell(B_, n));

            TCheck Who;

            if (dir == 1)
            {
                while ((B_ != TNote.A) && (n_ != 8))
                {
                    B_ = B_ - 1;
                    n_ = n_ + 1;

                    Pole.Who(new TCell(B_, n_), out Who);
                    if (Who == null)
                    {
                         arr.Add(new TCell(B_, n_));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (dir == 2)
            {
                while ((B_ != TNote.H) && (n_ != 8))
                {
                    B_ = B_ + 1;
                    n_ = n_ + 1;
                    Pole.Who(new TCell(B_, n_), out Who);
                    if (Who == null)
                    {
                        arr.Add(new TCell(B_, n_));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (dir == 3)
            {
                while ((B_ != TNote.A) && (n_ != 1))
                {
                    B_ = B_ - 1;
                    n_ = n_ - 1;
                    Pole.Who(new TCell(B_, n_), out Who);
                    if (Who == null)
                    {
                        arr.Add(new TCell(B_, n_));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (dir == 4)
            {
                while ((B_ != TNote.H) && (n_ != 1))
                {
                    B_ = B_ + 1;
                    n_ = n_ - 1;
                    Pole.Who(new TCell(B_, n_), out Who);
                    if (Who == null)
                    {
                        arr.Add(new TCell(B_, n_));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            TCell[] Res = null;

            if(arr.Count > 0)
            {
                Res = new TCell[arr.Count];

                for (int i = 0; i < arr.Count; i++)
                {
                    Res[i] = (TCell)arr[i];
                }
            }

            return Res;
        }

        public TCell NearDama(int dir, TPole Pole)
        {
            TNote B_ = B;
            int n_ = n;
            TCheck Who;

            if (dir == 1)
            {
                while ((B_ != TNote.A) && (n_ != 8))
                {
                    B_ = B_ - 1;
                    n_ = n_ + 1;
                    Pole.Who(new TCell(B_, n_), out Who);
                    if (Who != null)
                    {
                        return new TCell(B_, n_);
                    }
                }
            }

            if (dir == 2)
            {
                while ((B_ != TNote.H) && (n_ != 8))
                {
                    B_ = B_ + 1;
                    n_ = n_ + 1;
                    Pole.Who(new TCell(B_, n_), out Who);
                    if (Who != null)
                    {
                        return new TCell(B_, n_);
                    }
                }
            }

            if (dir == 3)
            {
                while ((B_ != TNote.A) && (n_ != 1))
                {
                    B_ = B_ - 1;
                    n_ = n_ - 1;
                    Pole.Who(new TCell(B_, n_), out Who);
                    if (Who != null)
                    {
                        return new TCell(B_, n_);
                    }
                }
            }

            if (dir == 4)
            {
                while ((B_ != TNote.H) && (n_ != 1))
                {
                    B_ = B_ + 1;
                    n_ = n_ - 1;
                    Pole.Who(new TCell(B_, n_), out Who);
                    if (Who != null)
                    {
                        return new TCell(B_, n_);
                    }
                }
            }

            return null;
        }

        public TCell Near(int dir)
        {
            if (dir == 1)
            {
                if ((B == TNote.A) || (n == 8))
                {
                    return null;
                }

                return new TCell((TNote)B - 1, n + 1);
            }

            if (dir == 2)
            {
                if ((B == TNote.H) || (n == 8))
                {
                    return null;
                }

                return new TCell((TNote)B + 1, n + 1);
            }

            if (dir == 3)
            {
                if ((B == TNote.A) || (n == 1))
                {
                    return null;
                }

                return new TCell((TNote)B - 1, n - 1);
            }

            if (dir == 4)
            {
                if ((B == TNote.H) || (n == 1))
                {
                    return null;
                }

                return new TCell((TNote)B + 1, n - 1);
            }

            return null;
        }

        public string ToString()
        {
            return B.ToString() + n.ToString();
        }

    }
}

