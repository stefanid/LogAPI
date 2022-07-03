# LogAPI
A small solution for creating a centralised logging REST API for legacy system. 
The project currently uses the following technologies:
1. Swagger for easy documentation and usage of the exposded API endpoints
2. FluentValidations to validate the inputs coming in and ensuring that the data is correct before being processed
3. .NET Core 6.0
4. NUNit for Unit testing
5. NBomber for Load testing

#Thought Process
Considering the solution has to be adjusted and be as decoupled as possible in order to safely without any impact on other modules integrate different solutions
to upload logging data to various destinations (MQ topic, Flatfile, Kafka topic, etc), the best and safest approach is to solve this with the usage of 
delegates. 
The usage of delegates helps in ensuring loose-coupling and safely scaling without too much of refactoring or impact. 
If I was to solve this with interfaces, I would end up having to create different interfaces for the different destinations that might come in play in the future
which would make scalability and readability a bit harder as an interface per destination would conform to the Single-responsibility pattern. Hence, why
for the sake of readability, easy scalability and maintainability, delagates were the perfect solution for the problem at hand.

Initially, I had two controllers to handle the two different logging destinations, however considering I am using a delegate that already creates the loose-coupling that I need,
having separate controllers for the different actions, made no sense.

For the extra bonus assignment - Load testing - I used NBomber to perform a load test on my code. I noticed that at 1000 concurrent users, my code was starting to fail,
and some of the requests were not handled. I am assuming the problem comes from the part of where I am writing directly to the file. To solve this this performance issue,
my solution would be to actually create a ConcurrencyQueue that will store the incoming requests and would implement a check on the queue that would validate
if the queue has the length of 1k or 5k requests (the smaller batcher, the better, as it would reduce the risk od data loss). Once the queue has reached the specified limit,
I would bulk write all the files asynchroniously, empty the ConcurrencyQueue and continue recieving requests.
