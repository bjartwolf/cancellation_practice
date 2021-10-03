# cancellation_practice
Just playing around with cancellations in .NET, a useful skill with Polly and friends

To run it is easiest to just test from the command line, for Test0 do:

They are not completed, it is very much wip
```
dotnet watch test --filter Test0
```
and when you manage to make the test green, run the same command with Test1 etc

Or run it with your favorite test runner, I prefer Visual Studio with NCrunch.

Main learning goals when it is completed is
What is cancellation token?
What is the cancellation token source?
What is a cancellationtoken.none?
What is Pollys cancellationtoken.default?

What happens when you cancel something?
How do you pass cancellationtokens around?
How do you combine them? How does polly do this?
