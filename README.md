<h1>ProjectBoxes</h1> 
This project is an assignment of Advanced Algorithms course. 

<h3>Project Description</h3>

The problem that is going to be solved in this project is about generating a sequence of boxes using dynamic programming. More specifically, we are given N number of 2 dimensional boxes including the width and length of every single one of them and are expected to find the largest set of boxes such that they can be put one into another.

To make the problem clearer:

<b>Given:</b>

• N: Number of 2-D rectangular boxes.

• Dimension of each box: (W,L).

<b>Goal:</b>

• Create a sequence of boxes as large as possible.

<b>Constraints:</b>

• One box can be inside the other only if the lower box has strictly higher dimensions. For example:

Box1 = (𝑊1, 𝐿1) and Box2 = (𝑊2, 𝐿2)

Box2 can be put into Box1 only if 𝑊1> 𝑊2and 𝐿1> 𝐿2

• The boxes can only be rotated horizontally or vertically, meaning that there will be no box put into another one diagonally. Since each box has 2 unique instances, the rotation can only be 90 degrees because 180 degrees will give us the same instance:

(W, L) -> (L, W) (90 degrees rotated)

(L, W) -> (W, L) (90 degrees rotated)

• None of the boxes contain more than one box inside even though two or more boxes could fit into some of them.

• Multiple instances of boxes are allowed.

<h3>Solution Description</h3>

To solve the problem with dynamic programming, the problem will be converted to a one of the most popular problems called Longest Increasing Subsequence (LIS). LIS is a very similar problem to ours but with only couple of differences. Let us first describe how we are going to make our problem look like LIS then move onto explain the details of this conversion and LIS itself. Note that, it is assumed that starting indexes of all the arrays in the document is 1 not 0, for simplicity.

<h4>Converting The Problem</h4>

In the problem we are asked to find longest sequence and this sequence must be in decreasing order since the boxes will be put into one in another. The idea is very similar to LIS, but in LIS it is important that the sequence preserves the positions of the elements in correct order. In our case the order of the boxes is not important. Thus, to be able to use LIS we need to do 2 operations at first:

• Generate an array of N elements containing the dimensions of boxes including rotated ones with the following rule for box i where i∈{1,2,3,…,𝑁}:

    o The first coordinate will be considered as width and it will take the max of (𝑊𝑖, 𝐿𝑖)
    
    o The second will be considered as length and it will take the min of (𝑊𝑖, 𝐿𝑖)
    
By this way we generate our array using the necessary rotated versions of the boxes and avoid using unnecessary space.

• Sort our array in decreasing order with merge sort. The process is like sorting the boxes and putting them in an order so that they can be chosen in preserved order as well, just like a sequence in LIS. Sorting will be according to first coordinate, which stands for width, i.e., maximum of the dimensions. By doing this we are making sure that whichever sequence of boxes we choose, the widths will always preserve the decreasing order, hence, the problem becomes only applying LIS according to second coordinate, which is length, i.e., minimum of the dimensions.
