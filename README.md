# BingoPlateGenerator
Assignment from Specialists.<br>
> Alright, let’s test that! You are going to build a bingo plate generator, which requires a bit more math than most people think. You will code it in whatever language and on whatever platform you want/can, but it will need 2 input fields, and it will generate a series of printouts.<br>
> The first input field is title/serializer, that will be printed on each bingo plate. That way we can print custom plates for an event, and should we print several plates, we can tell them apart, so we don’t by accident pass out identical plates. The second input field tells us how many plates we want, if we input 10 it makes 10 unique plates, if we input 5000 it generates 5000 unique plates.
> 
> Each plate has three rows and nine columns. In each column is one or two numbers, placed randomly, the smallest one first. In the first column is numbers from 1 to 9, in the next is 10 to 19, the next is 20 to 29 and so on. The last is 80 to 90.
> 
> In total 15 numbers are printed on each plate, and 12 are empty. The positions of these empty ones are as mentioned random, but make sure you do not put all the double digits in the first rows, it should be “proper random”. 
> 
> Now randomizing 15 numbers and the positions of 12 empty spaces might not sound too hard, but you also have to figure out a system of storing them, because if a manufacturer wants to print 200’000’000 of them, they have to guarantee that each plate will be unique, no set of identical numbered plates can exists. So you don’t have to store the position of the 12 empty spots at least, but it still does complicate things a bit.
> But really this is not as easy as some beginners think. It can be done elegantly with nested loops. Certainly we do not want 500 IF cases, but HOW you choose to solve it up to you entirely. Good luck on your mission, and this message will not self-destruct in five seconds!

## BingoPlateGenerator
Main project, very basic frontend.

## ConsoleBingoPlateGenerator
Prototype, very rough.
