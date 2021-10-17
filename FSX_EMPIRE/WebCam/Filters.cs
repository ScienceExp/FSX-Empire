namespace WebCam
{
    /// <summary>Convolution Filters</summary>
    public class Filters
    {
        public enum Matrix
        {
            Identity3x3,
            Laplacian3x3,
            Laplacian5x5,
            LaplacianOfGaussian,
            Gaussian3x3,
            Gaussian5x5Type1,
            Gaussian5x5Type2,
            Sobel3x3Horizontal,
            Sobel3x3Vertical,
            Prewitt3x3Horizontal,
            Prewitt3x3Vertical,
            Kirsch3x3Horizontal,
            Kirsch3x3Vertical,
            Edge1,
            Edge2
        }

        /// <summary>Returns a double[,] matrix from the filter enum name</summary>
        public static double[,] Get(Matrix filter)
        {
            switch (filter)
            {
                case Matrix.Laplacian3x3:
                    return Laplacian3x3;
                case Matrix.Laplacian5x5:
                    return Laplacian5x5;
                case Matrix.LaplacianOfGaussian:
                    return LaplacianOfGaussian;
                case Matrix.Gaussian3x3:
                    return Gaussian3x3;
                case Matrix.Gaussian5x5Type1:
                    return Gaussian5x5Type1;
                case Matrix.Gaussian5x5Type2:
                    return Gaussian5x5Type2;
                case Matrix.Sobel3x3Horizontal:
                    return Sobel3x3Horizontal;
                case Matrix.Sobel3x3Vertical:
                    return Sobel3x3Vertical;
                case Matrix.Prewitt3x3Horizontal:
                    return Prewitt3x3Horizontal;
                case Matrix.Prewitt3x3Vertical:
                    return Prewitt3x3Vertical;
                case Matrix.Kirsch3x3Horizontal:
                    return Kirsch3x3Horizontal;
                case Matrix.Kirsch3x3Vertical:
                    return Kirsch3x3Vertical;
                case Matrix.Edge1:
                    return Edge1;
                case Matrix.Edge2:
                    return Edge2;
                default:
                    return Identity3x3;
            }
        }

        #region Test Matrix's
        public static double[,] Edge1
        {
            get
            {
                return new double[,]
                { { -3,  -5,  0, },
                  { -5,  0,  5, },
                  { 0,  5,  3, }, };
            }
        }
        public static double[,] Edge2
        {
            get
            {
                return new double[,]
                { { 3,  5,  0, },
                  { 5,  0,  -5, },
                  { 0,  -5,  -3, }, };
            }
        }
        #endregion

        #region Filter Matrix's
        public static double[,] Identity3x3
        {
            get
            {
                return new double[,]
                { { 0, 0, 0,  },
                  { 0, 1, 0,  },
                  { 0, 0, 0,  }, };
            }
        }

        public static double[,] Laplacian3x3
        {
            get
            {
                return new double[,]
                { { -1, -1, -1,  },
                  { -1,  8, -1,  },
                  { -1, -1, -1,  }, };
            }
        }

        public static double[,] Laplacian5x5
        {
            get
            {
                return new double[,]
                { { -1, -1, -1, -1, -1, },
                  { -1, -1, -1, -1, -1, },
                  { -1, -1, 24, -1, -1, },
                  { -1, -1, -1, -1, -1, },
                  { -1, -1, -1, -1, -1  }, };
            }
        }

        public static double[,] LaplacianOfGaussian
        {
            get
            {
                return new double[,]
                { {  0,   0, -1,  0,  0 },
                  {  0,  -1, -2, -1,  0 },
                  { -1,  -2, 16, -2, -1 },
                  {  0,  -1, -2, -1,  0 },
                  {  0,   0, -1,  0,  0 }, };
            }
        }

        public static double[,] Gaussian3x3
        {
            get
            {
                return new double[,]
                { { 1, 2, 1, },
                  { 2, 4, 2, },
                  { 1, 2, 1, }, };
            }
        }

        public static double[,] Gaussian5x5Type1
        {
            get
            {
                return new double[,]
                { { 2, 04, 05, 04, 2 },
                  { 4, 09, 12, 09, 4 },
                  { 5, 12, 15, 12, 5 },
                  { 4, 09, 12, 09, 4 },
                  { 2, 04, 05, 04, 2 }, };
            }
        }

        public static double[,] Gaussian5x5Type2
        {
            get
            {
                return new double[,]
                { {  1,   4,  6,  4,  1 },
                  {  4,  16, 24, 16,  4 },
                  {  6,  24, 36, 24,  6 },
                  {  4,  16, 24, 16,  4 },
                  {  1,   4,  6,  4,  1 }, };
            }
        }

        public static double[,] Sobel3x3Horizontal
        {
            get
            {
                return new double[,]
                { { -1,  0,  1, },
                  { -2,  0,  2, },
                  { -1,  0,  1, }, };
            }
        }

        public static double[,] Sobel3x3Vertical
        {
            get
            {
                return new double[,]
                { {  1,  2,  1, },
                  {  0,  0,  0, },
                  { -1, -2, -1, }, };
            }
        }

        public static double[,] Prewitt3x3Horizontal
        {
            get
            {
                return new double[,]
                { { -1,  0,  1, },
                  { -1,  0,  1, },
                  { -1,  0,  1, }, };
            }
        }

        public static double[,] Prewitt3x3Vertical
        {
            get
            {
                return new double[,]
                { {  1,  1,  1, },
                  {  0,  0,  0, },
                  { -1, -1, -1, }, };
            }
        }

        public static double[,] Kirsch3x3Horizontal
        {
            get
            {
                return new double[,]
                { {  5,  5,  5, },
                  { -3,  0, -3, },
                  { -3, -3, -3, }, };
            }
        }

        public static double[,] Kirsch3x3Vertical
        {
            get
            {
                return new double[,]
                { {  5, -3, -3, },
                  {  5,  0, -3, },
                  {  5, -3, -3, }, };
            }
        }
        #endregion
    }
}
