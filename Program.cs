using System;

namespace bezier_curve_calculator {
    class Program {
        static BezierCurve bc = new BezierCurve();

        // Bernstein coeficients used to construct the Bernstein polynomial
        static decimal[] B;

        // The points in the Casteljau's Algorithm
        static Point[][] CP;

        static void PrintSeparator() {
            Console.WriteLine("-----------------------------");
        }

        static void Input() {
            Console.Write("Enter the degree n = ");
            bc.n = int.Parse(Console.ReadLine());

            Console.Write("Enter the argument u = ");
            bc.u = decimal.Parse(Console.ReadLine());

            bc.controlPoints = new Point[bc.n+1];
            B = new decimal[bc.n + 1];
            CP = new Point[bc.n + 1][];
            Console.WriteLine();

            Console.WriteLine("Enter the coordinates of each control point separated by a single space");
            for (int i = 0; i <= bc.n; i++) {
                Console.Write($"P{i}: ");

                string[] coordinates = new string[2];
                coordinates = Console.ReadLine().Split(' ');

                bc.controlPoints[i] = new Point();
                bc.controlPoints[i].x = decimal.Parse(coordinates[0]);
                bc.controlPoints[i].y = decimal.Parse(coordinates[1]);
            }
        }

        static void CalculateBernsteinCoefs() {
            PrintSeparator();

            decimal sum = 0;
            decimal xu = 0, yu = 0;

            Console.WriteLine("Natural parametrisation: ");

            for(int i = 0; i <= bc.n; i++) {
                decimal FactorialCoef = Fact(bc.n) / (Fact(i) * Fact(bc.n - i));

                // displaying the equation in its general case 
                Console.Write($"{FactorialCoef}.u^{i}.(1-u)^{bc.n-i}.({bc.controlPoints[i].x}, {bc.controlPoints[i].y})");
                if( i < bc.n ) {
                    Console.Write("  +  ");
                }

                B[i] = Math.Round(FactorialCoef * Pow(bc.u, i) * Pow(1-bc.u, bc.n - i), 4);
                sum += B[i];
                xu += B[i] * bc.controlPoints[i].x;
                yu += B[i] * bc.controlPoints[i].y;
            }

            Console.WriteLine();
            Console.WriteLine($"C(u) = (x(u), y(u))");
            Console.WriteLine($"C(u = 0,25) = ({xu}, {yu})");
            
            PrintSeparator();
            Console.WriteLine("Bernstein Coeficients");

            for (int i = 0; i <= bc.n; i++) {
                Console.WriteLine($"B{bc.n},{i} = {B[i]}");
            }

            Console.WriteLine($"Sum: {sum}");
            PrintSeparator();
        }

        static void CasteljauAlgorithm() {
            Console.WriteLine("Casteljau's Algorithm Points");

            for (int i = 0; i <= bc.n; i++) {
                int rowSize = bc.n - i + 1;
                CP[i] = new Point[rowSize];

                for (int j = 0; j < rowSize; j++) {
                    CP[i][j] = new Point();
                    if (i == 0) {
                        CP[i][j].x = bc.controlPoints[j].x;
                        CP[i][j].y = bc.controlPoints[j].y;
                        Console.WriteLine($"P{j} =({CP[i][j].x}, {CP[i][j].y})");
                    }
                    else {
                        CP[i][j].x = Math.Round((1 - bc.u) * CP[i - 1][j].x + bc.u * CP[i - 1][j + 1].x, 4);
                        CP[i][j].y = Math.Round((1 - bc.u) * CP[i - 1][j].y + bc.u * CP[i - 1][j + 1].y, 4);
                        Console.WriteLine($"P{i}{j}=({CP[i][j].x}, {CP[i][j].y})");
                    }
                }
                Console.WriteLine();
            }
        }
        
        static void PrintLeftRightSegments() {
            PrintSeparator();
            Console.WriteLine($"Points in left segment where u in [0; {bc.u}]:");
            for (int i = 0; i <= bc.n; i++) {
                if (i == 0) {
                    Console.Write($"P{i}({CP[i][0].x}; {CP[i][0].y}), ");
                }
                else {
                    Console.Write($"P{i}0({CP[i][0].x}; {CP[i][0].y})");
                    if(i != bc.n) {
                        Console.Write(", ");
                    }
                    else {
                        Console.WriteLine();
                    }
                }
            }

            PrintSeparator();
            Console.WriteLine($"Points in right segment where u in [{bc.u}; 1]:");
            for (int i = 0; i <= bc.n; i++) {
                if(i == bc.n ) {
                    Console.Write($"P{i}({CP[bc.n-i][i].x}; {CP[bc.n-i][i].y})\n");
                }
                else {
                    Console.Write($"P{bc.n-i},{i}({CP[bc.n - i][i].x}; {CP[bc.n - i][i].y}), ");
                }
            }
        }

        static void RaiseDegree() {
            Console.Write("How many times do you wish to raise the degree of the curve? (Enter 0 for 'No'): ");
            int numberOfRaises = int.Parse(Console.ReadLine());

            BezierCurve currentCurve = bc;
            for(int i = 1; i <= numberOfRaises; i++) {
                BezierCurve raisedCurve = new BezierCurve();
                raisedCurve.n = currentCurve.n + 1;
                raisedCurve.u = currentCurve.u;
                raisedCurve.controlPoints = new Point[raisedCurve.n + 1];

                Console.WriteLine($"Curve degree raised {i} time(s). New control points are:");
                for(int j = 0; j <= raisedCurve.n; j ++) {
                    raisedCurve.controlPoints[j] = new Point();

                    if(j == 0) {
                        raisedCurve.controlPoints[0].x = currentCurve.controlPoints[0].x;
                        raisedCurve.controlPoints[0].y = currentCurve.controlPoints[0].y;
                    }
                    else if(j == raisedCurve.n) {
                        raisedCurve.controlPoints[j].x = currentCurve.controlPoints[j - 1].x;
                        raisedCurve.controlPoints[j].y = currentCurve.controlPoints[j - 1].y;
                    }
                    else {
                        raisedCurve.controlPoints[j].x = Math.Round((((decimal)j / raisedCurve.n) * currentCurve.controlPoints[j - 1].x) + (1 - ((decimal)j /raisedCurve.n))*currentCurve.controlPoints[j].x, 4);
                        raisedCurve.controlPoints[j].y = Math.Round(((((decimal)j / raisedCurve.n) * currentCurve.controlPoints[j - 1].y)) + (1 - ((decimal)j /raisedCurve.n))*currentCurve.controlPoints[j].y, 4);
                    }
                    Console.WriteLine($"P{j} ({raisedCurve.controlPoints[j].x}, {raisedCurve.controlPoints[j].y})");
                }
                PrintSeparator();
                currentCurve = raisedCurve;
            }
        }

        static long Fact(int n) {
            long fact = 1;
            for(int i = 1; i <= n; i ++ ) {
                fact *= i;
            }
            return fact;
        }
        
        static decimal Pow(decimal num, int power) {
            if(power == 0) {
                return 1;
            }

            decimal n = num;
            for(int i = 1; i < power; i ++) {
                n *= num;
            }
            return n;
        }

        static void Main(string[] args) {
            Input();
            CalculateBernsteinCoefs();
            CasteljauAlgorithm();
            PrintLeftRightSegments();
            RaiseDegree();
        }
    }
}
