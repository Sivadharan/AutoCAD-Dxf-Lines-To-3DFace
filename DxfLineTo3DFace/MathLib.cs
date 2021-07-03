using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib
{
	public class Pnt
	{
		private decimal _x, _y, _z;
		public decimal x
		{
			get
			{
				return _x;
			}
			set
			{
				AssignValues(value, this._y, this._z);
			}
		}

		public decimal y
		{
			get
			{
				return _y;
			}
			set
			{
				AssignValues(this._x, value, this._z);
			}
		}

		public decimal z
		{
			get
			{
				return _z;
			}
			set
			{
				AssignValues(this._x, this._y, value);
			}
		}

		private List<decimal> _xyz;

		public Pnt()
		{
			AssignValues(0, 0, 0);
		}

		public Pnt(decimal x, decimal y, decimal z)
		{
			AssignValues(x, y, z);
		}

		private void AssignValues(decimal x, decimal y, decimal z)
		{
			this._x = x;
			this._y = y;
			this._z = z;
			_xyz = new List<decimal>();
			_xyz.Add(x);
			_xyz.Add(y);
			_xyz.Add(z);
		}
		public List<decimal> xyz
		{
			get
			{
				return _xyz;
			}
			set
			{
				AssignValues(value[0], value[1], value[2]);
			}
		}
	}

	public class Line
	{
		public int id;

		public Pnt p1, p2;
		public Line(decimal x1, decimal y1, decimal z1,
					decimal x2, decimal y2, decimal z2)
		{
			p1 = new Pnt(x1, y1, z1);
			p2 = new Pnt(x2, y2, z2);
		}

		public Line(Pnt p1, Pnt p2)
		{
			AssignValues(p1, p2);
		}

		private void AssignValues(Pnt p1, Pnt p2)
		{
			this.p1 = p1;
			this.p2 = p2;
		}

		public decimal Length
		{
			get
			{
				return Utils.GetDistance(p1, p2);
			}
		}

		//public Line Reverse
		//{
		//	get
		//	{
		//		return new Line(this.p2, this.p1);
		//	}
		//}

	}

	public class ThreeDFace
	{
		public List<Pnt> Pnts;
	}

	public class ThreeDFaceBuilder
	{
		private List<Pnt> _Pnts;
		public List<Pnt> Pnts
		{
			get
			{
				return _Pnts;
			}
		}

		public Pnt LastPnt
		{
			get
			{
				if ((_Pnts != null) && (_Pnts.Count > 0))
				{
					return _Pnts[_Pnts.Count - 1];
				}
				else
					return new Pnt(0, 0, 0);
			}
		}

		public Pnt PreLastPnt
		{
			get
			{
				if ((_Pnts != null) && (_Pnts.Count >= 2))
				{
					return _Pnts[_Pnts.Count - 2];
				}
				else
					return new Pnt(0, 0, 0);
			}
		}

		public bool FaceComplete
		{
			get
			{
				if ((_Pnts != null) && (_Pnts.Count >= 4))
				{
					return Utils.GetDistance(_Pnts[0], _Pnts[_Pnts.Count - 1]) < Utils.Tolerance;
				}
				else
					return false;
			}
		}

		public void AddPoints(Pnt Pnt)
		{
			if (FaceComplete == false)
			{
				if (_Pnts == null)
				{
					_Pnts = new List<Pnt>();
				}
				if (_Pnts.Count <= 4)
				{
					_Pnts.Add(Pnt);
				}
			}
		}

		public ThreeDFace GetThreeDFace()
		{
			ThreeDFace ThreeDFace = new ThreeDFace();
			ThreeDFace.Pnts = new List<Pnt>();
			foreach (Pnt Pnt in _Pnts)
			{
				ThreeDFace.Pnts.Add(Pnt);
			}
			return ThreeDFace;
		}
	}

	public static class Utils
	{
		public static decimal Tolerance = 5;
		public static decimal GetDistance(Pnt p1, Pnt p2)
		{
			return (Decimal) Math.Sqrt(Math.Pow((Double) (p1.x - p2.x), 2) +
				 Math.Pow((Double) (p1.y - p2.y), 2) +
				 Math.Pow((Double) (p1.z - p2.z), 2));
		}

		public static bool IsSamePoint(Pnt p1, Pnt p2)
		{
			return (GetDistance(p1, p2) < Tolerance);
		}

		public static bool IsSameLine(Line Line1, Line Line2)
		{
			return ((GetDistance(Line1.p1, Line2.p1) < Tolerance) &&
				   (GetDistance(Line1.p2, Line2.p2) < Tolerance)) ||
				   ((GetDistance(Line1.p1, Line2.p2) < Tolerance) &&
				   (GetDistance(Line1.p2, Line2.p1) < Tolerance));
		}

		public static bool PointInLine(Line Line, Pnt Pnt)
		{
			return (GetDistance(Line.p1, Pnt) < Tolerance) ||
				   (GetDistance(Line.p2, Pnt) < Tolerance);
		}

		public static Pnt AnotherPointInLine(Line Line, Pnt Pnt)
		{
			Pnt AnotherPnt = new Pnt(0, 0, 0);
			if (GetDistance(Line.p1, Pnt) < Tolerance)
			{
				AnotherPnt = Line.p2;
			}
			else if (GetDistance(Line.p2, Pnt) < Tolerance)
			{
				AnotherPnt = Line.p1;
			}
			return AnotherPnt;
		}
	}

}