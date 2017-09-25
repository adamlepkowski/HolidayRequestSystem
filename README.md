# HolidayRequestSystem
Simple holiday request application created using CQRS and ES approach.

###### Key information:
- I develop this example in my free time with an approach where I focus on providing a "business value" as fast as I can. It means that you can find a lot of TODO in a comment, but I frequently clean code and make some refactoring.
- Event store is on a SQL Server. There is no eventual consistency - read model is saved with event store information in one transaction.
- Model structure can be designed in the better way ( holiday request can be a separate AR what improves performance and scalability options) but I will do it in the further implementation because what I implement here is much closer to another scenario I have.