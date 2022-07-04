<h1>ProjectBoxes</h1> 
This project is an assignment of Advanced Algorithms course. 

<h3>Project Description</h3>

The problem that is going to be solved in this project is about generating a sequence of boxes using dynamic programming. More specifically, we are given N number of 2 dimensional boxes including the width and length of every single one of them and are expected to find the largest set of boxes such that they can be put one into another.
To make the problem clearer:
Given:
• N: Number of 2-D rectangular boxes.
• Dimension of each box: (W,L).
Goal:
• Create a sequence of boxes as large as possible.
Constraints:
• One box can be inside the other only if the lower box has strictly higher dimensions. For example:
Box1 = (𝑊1, 𝐿1) and Box2 = (𝑊2, 𝐿2)
Box2 can be put into Box1 only if 𝑊1> 𝑊2and 𝐿1> 𝐿2
• The boxes can only be rotated horizontally or vertically, meaning that there will be no box put into another one diagonally. Since each box has 2 unique instances, the rotation can only be 90 degrees because 180 degrees will give us the same instance:
(W, L) -> (L, W) (90 degrees rotated)
(L, W) -> (W, L) (90 degrees rotated)
• None of the boxes contain more than one box inside even though two or more boxes could fit into some of them.
• Multiple instances of boxes are allowed.
