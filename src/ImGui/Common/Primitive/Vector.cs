using System;

namespace ImGui
{
    /// <summary>
    /// Vector - A value type which defined a vector in terms of X and Y
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("X:{X} Y:{Y}")]
    [System.Diagnostics.DebuggerStepThrough]
    [Serializable]
    public struct Vector : IFormattable
    {
        #region Constructors

        /// <summary>
        /// Constructor which sets the vector's initial values
        /// </summary>
        /// <param name="x"> float - The initial X </param>
        /// <param name="y"> float - THe initial Y </param>
        public Vector(float x, float y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Constructor which sets the vector's initial values
        /// </summary>
        /// <param name="x"> double - The initial X </param>
        /// <param name="y"> double - THe initial Y </param>
        public Vector(double x, double y)
        {
            _x = (float)x;
            _y = (float)y;
        }

        #endregion

        /// <summary>
        /// Length Property - the length of this Vector
        /// </summary>
        public double Length
        {
            get
            {
                return System.Math.Sqrt(_x * _x + _y * _y);
            }
        }

        /// <summary>
        /// LengthSquared Property - the squared length of this Vector
        /// </summary>
        public double LengthSquared
        {
            get
            {
                return _x * _x + _y * _y;
            }
        }

        /// <summary>
        /// Normalize - Updates this Vector to maintain its direction, but to have a length
        /// of 1.  This is equivalent to dividing this Vector by Length
        /// </summary>
        public void Normalize()
        {
            // Avoid overflow
            this /= System.Math.Max(System.Math.Abs(_x), System.Math.Abs(_y));
            this /= Length;
        }

        /// <summary>
        /// CrossProduct - Returns the cross product: vector1.X*vector2.Y - vector1.Y*vector2.X
        /// </summary>
        /// <returns>
        /// Returns the cross product: vector1.X*vector2.Y - vector1.Y*vector2.X
        /// </returns>
        /// <param name="vector1"> The first Vector </param>
        /// <param name="vector2"> The second Vector </param>
        public static double CrossProduct(Vector vector1, Vector vector2)
        {
            return vector1._x * vector2._y - vector1._y * vector2._x;
        }

        /// <summary>
        /// AngleBetween - the angle between 2 vectors
        /// </summary>
        /// <returns>
        /// Returns the the angle in degrees between vector1 and vector2
        /// </returns>
        /// <param name="vector1"> The first Vector </param>
        /// <param name="vector2"> The second Vector </param>
        public static double AngleBetween(Vector vector1, Vector vector2)
        {
            double sin = vector1._x * vector2._y - vector2._x * vector1._y;
            double cos = vector1._x * vector2._x + vector1._y * vector2._y;

            return System.Math.Atan2(sin, cos) * (180 / System.Math.PI);
        }

        /// <summary>
        /// Operator -Vector (unary negation)
        /// </summary>
        public static Vector operator -(Vector vector)
        {
            return new Vector(-vector._x, -vector._y);
        }

        /// <summary>
        /// Negates the values of X and Y on this Vector
        /// </summary>
        public void Negate()
        {
            _x = -_x;
            _y = -_y;
        }

        /// <summary>
        /// Operator Vector + Vector
        /// </summary>
        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return new Vector(vector1._x + vector2._x,
                              vector1._y + vector2._y);
        }

        /// <summary>
        /// Add: Vector + Vector
        /// </summary>
        public static Vector Add(Vector vector1, Vector vector2)
        {
            return new Vector(vector1._x + vector2._x,
                              vector1._y + vector2._y);
        }

        /// <summary>
        /// Operator Vector - Vector
        /// </summary>
        public static Vector operator -(Vector vector1, Vector vector2)
        {
            return new Vector(vector1._x - vector2._x,
                              vector1._y - vector2._y);
        }

        /// <summary>
        /// Subtract: Vector - Vector
        /// </summary>
        public static Vector Subtract(Vector vector1, Vector vector2)
        {
            return new Vector(vector1._x - vector2._x,
                              vector1._y - vector2._y);
        }

        /// <summary>
        /// Operator Vector + Point
        /// </summary>
        public static Point operator +(Vector vector, Point point)
        {
            return new Point(point._x + vector._x, point._y + vector._y);
        }

        /// <summary>
        /// Add: Vector + Point
        /// </summary>
        public static Point Add(Vector vector, Point point)
        {
            return new Point(point._x + vector._x, point._y + vector._y);
        }

        /// <summary>
        /// Operator Vector * double
        /// </summary>
        public static Vector operator *(Vector vector, double scalar)
        {
            return new Vector(vector._x * scalar,
                              vector._y * scalar);
        }

        /// <summary>
        /// Multiply: Vector * double
        /// </summary>
        public static Vector Multiply(Vector vector, double scalar)
        {
            return new Vector(vector._x * scalar,
                              vector._y * scalar);
        }

        /// <summary>
        /// Operator double * Vector
        /// </summary>
        public static Vector operator *(double scalar, Vector vector)
        {
            return new Vector(vector._x * scalar,
                              vector._y * scalar);
        }

        /// <summary>
        /// Multiply: double * Vector
        /// </summary>
        public static Vector Multiply(double scalar, Vector vector)
        {
            return new Vector(vector._x * scalar,
                              vector._y * scalar);
        }

        /// <summary>
        /// Operator Vector / double
        /// </summary>
        public static Vector operator /(Vector vector, double scalar)
        {
            return vector * (1.0f / scalar);
        }

        /// <summary>
        /// Multiply: Vector / double
        /// </summary>
        public static Vector Divide(Vector vector, double scalar)
        {
            return vector * (1.0f / scalar);
        }

#if Matrix
        /// <summary>
        /// Operator Vector * Matrix
        /// </summary>
        public static Vector operator * (Vector vector, Matrix matrix)
        {
            return matrix.Transform(vector);
        }

        /// <summary>
        /// Multiply: Vector * Matrix
        /// </summary>
        public static Vector Multiply(Vector vector, Matrix matrix)
        {
            return matrix.Transform(vector);
        }
#endif

        /// <summary>
        /// Operator Vector * Vector, interpreted as their dot product
        /// </summary>
        public static double operator *(Vector vector1, Vector vector2)
        {
            return vector1._x * vector2._x + vector1._y * vector2._y;
        }

        /// <summary>
        /// Multiply - Returns the dot product: vector1.X*vector2.X + vector1.Y*vector2.Y
        /// </summary>
        /// <returns>
        /// Returns the dot product: vector1.X*vector2.X + vector1.Y*vector2.Y
        /// </returns>
        /// <param name="vector1"> The first Vector </param>
        /// <param name="vector2"> The second Vector </param>
        public static double Multiply(Vector vector1, Vector vector2)
        {
            return vector1._x * vector2._x + vector1._y * vector2._y;
        }

        /// <summary>
        /// Determinant - Returns the determinant det(vector1, vector2)
        /// </summary>
        /// <returns>
        /// Returns the determinant: vector1.X*vector2.Y - vector1.Y*vector2.X
        /// </returns>
        /// <param name="vector1"> The first Vector </param>
        /// <param name="vector2"> The second Vector </param>
        public static double Determinant(Vector vector1, Vector vector2)
        {
            return vector1._x * vector2._y - vector1._y * vector2._x;
        }

        /// <summary>
        /// Explicit conversion to Size.  Note that since Size cannot contain negative values,
        /// the resulting size will contains the absolute values of X and Y
        /// </summary>
        /// <returns>
        /// Size - A Size equal to this Vector
        /// </returns>
        /// <param name="vector"> Vector - the Vector to convert to a Size </param>
        public static explicit operator Size(Vector vector)
        {
            return new Size(System.Math.Abs(vector._x), System.Math.Abs(vector._y));
        }

        /// <summary>
        /// Explicit conversion to Point
        /// </summary>
        /// <returns>
        /// Point - A Point equal to this Vector
        /// </returns>
        /// <param name="vector"> Vector - the Vector to convert to a Point </param>
        public static explicit operator Point(Vector vector)
        {
            return new Point(vector._x, vector._y);
        }

        public static readonly Vector Zero = new Vector();

        /// <summary>
        /// Compares two Vector instances for exact equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, double.NaN is not equal to itself.
        /// </summary>
        /// <returns>
        /// bool - true if the two Vector instances are exactly equal, false otherwise
        /// </returns>
        /// <param name='vector1'>The first Vector to compare</param>
        /// <param name='vector2'>The second Vector to compare</param>
        public static bool operator == (Vector vector1, Vector vector2)
        {
            return vector1.X == vector2.X &&
                   vector1.Y == vector2.Y;
        }

        /// <summary>
        /// Compares two Vector instances for exact inequality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, double.NaN is not equal to itself.
        /// </summary>
        /// <returns>
        /// bool - true if the two Vector instances are exactly unequal, false otherwise
        /// </returns>
        /// <param name='vector1'>The first Vector to compare</param>
        /// <param name='vector2'>The second Vector to compare</param>
        public static bool operator != (Vector vector1, Vector vector2)
        {
            return !(vector1 == vector2);
        }
        /// <summary>
        /// Compares two Vector instances for object equality.  In this equality
        /// double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if the two Vector instances are exactly equal, false otherwise
        /// </returns>
        /// <param name='vector1'>The first Vector to compare</param>
        /// <param name='vector2'>The second Vector to compare</param>
        public static bool Equals (Vector vector1, Vector vector2)
        {
            return vector1.X.Equals(vector2.X) &&
                   vector1.Y.Equals(vector2.Y);
        }

        /// <summary>
        /// Equals - compares this Vector with the passed in object.  In this equality
        /// double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if the object is an instance of Vector and if it's equal to "this".
        /// </returns>
        /// <param name='o'>The object to compare to "this"</param>
        public override bool Equals(object o)
        {
            if ((null == o) || !(o is Vector))
            {
                return false;
            }

            Vector value = (Vector)o;
            return Vector.Equals(this,value);
        }

        /// <summary>
        /// Equals - compares this Vector with the passed in object.  In this equality
        /// double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if "value" is equal to "this".
        /// </returns>
        /// <param name='value'>The Vector to compare to "this"</param>
        public bool Equals(Vector value)
        {
            return Vector.Equals(this, value);
        }
        /// <summary>
        /// Returns the HashCode for this Vector
        /// </summary>
        /// <returns>
        /// int - the HashCode for this Vector
        /// </returns>
        public override int GetHashCode()
        {
            // Perform field-by-field XOR of HashCodes
            return X.GetHashCode() ^
                   Y.GetHashCode();
        }

        /// <summary>
        /// Parse - returns an instance converted from the provided string using
        /// the culture "en-US"
        /// <param name="source"> string with Vector data </param>
        /// </summary>
        public static Vector Parse(string source)
        {
            throw new NotImplementedException();
#if false
            IFormatProvider formatProvider = System.Windows.Markup.TypeConverterHelper.InvariantEnglishUS;

            TokenizerHelper th = new TokenizerHelper(source, formatProvider);

            Vector value;

            String firstToken = th.NextTokenRequired();

            value = new Vector(
                Convert.ToDouble(firstToken, formatProvider),
                Convert.ToDouble(th.NextTokenRequired(), formatProvider));

            // There should be no more tokens in this string.
            th.LastTokenRequired();

            return value;
#endif
        }

        /// <summary>
        ///     X - double.  Default value is 0.
        /// </summary>
        public double X
        {
            get
            {
                return _x;
            }

            set
            {
                _x = (float)value;
            }

        }

        /// <summary>
        ///     Y - double.  Default value is 0.
        /// </summary>
        public double Y
        {
            get
            {
                return _y;
            }

            set
            {
                _y = (float)value;
            }

        }

        /// <summary>
        /// Creates a string representation of this object based on the current culture.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public override string ToString()
        {

            // Delegate to the internal method which implements all ToString calls.
            return ConvertToString(null /* format string */, null /* format provider */);
        }

        /// <summary>
        /// Creates a string representation of this object based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public string ToString(IFormatProvider provider)
        {

            // Delegate to the internal method which implements all ToString calls.
            return ConvertToString(null /* format string */, provider);
        }

        /// <summary>
        /// Creates a string representation of this object based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        string IFormattable.ToString(string format, IFormatProvider provider)
        {

            // Delegate to the internal method which implements all ToString calls.
            return ConvertToString(format, provider);
        }

        /// <summary>
        /// Creates a string representation of this object based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            return String.Format(provider,
                                 "{0:" + format + "},{1:" + format + "}",
                                 _x,
                                 _y);
        }


        internal float _x;
        internal float _y;

    }
}
