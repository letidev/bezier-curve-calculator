# Example
```html
Enter the degree n = 3
Enter the argument u = 0,25

Enter the coordinates of each control point separated by a single space
P0: 0 4
P1: 4 0
P2: -4 -4
P3: -4 0
-----------------------------
Natural parametrisation:
1.u^0.(1-u)^3.(0, 4)  +  3.u^1.(1-u)^2.(4, 0)  +  3.u^2.(1-u)^1.(-4, -4)  +  1.u^3.(1-u)^0.(-4, 0)
C(u) = (x(u), y(u))
C(u = 0,25) = (1,0628, 1,1252)
-----------------------------
Bernstein Coeficients
B3,0 = 0,4219
B3,1 = 0,4219
B3,2 = 0,1406
B3,3 = 0,0156
Sum: 1,0000
-----------------------------
Casteljau's Algorithm Points
P0 =(0, 4)
P1 =(4, 0)
P2 =(-4, -4)
P3 =(-4, 0)

P10=(1,00, 3,00)
P11=(2,00, -1,00)
P12=(-4,00, -3,00)

P20=(1,2500, 2,0000)
P21=(0,5000, -1,5000)

P30=(1,0625, 1,1250)

-----------------------------
Points in left segment where u in [0; 0,25]:
P0(0; 4), P10(1,00; 3,00), P20(1,2500; 2,0000), P30(1,0625; 1,1250)
-----------------------------
Points in right segment where u in [0,25; 1]:
P3,0(1,0625; 1,1250), P2,1(0,5000; -1,5000), P1,2(-4,00; -3,00), P3(-4; 0)
-----------------------------
How many times do you wish to raise the degree of the curve? (Enter 0 for 'No'): 1
Curve degree raised 1 time(s). New control points are:
P0 (0, 4)
P1 (3,00, 1,00)
P2 (0,0, -2,0)
P3 (-4,00, -3,00)
P4 (-4, 0)
-----------------------------
```