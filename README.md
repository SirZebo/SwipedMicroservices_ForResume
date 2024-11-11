# SwipedMicroservices
- Swiped! is a real-time e-bidding system that provide real-time bid update to clients.
- This project is still in progress and is separated from my cloud and distributed system project submission repo

System Design
![image](https://github.com/user-attachments/assets/33910cb9-3c22-403c-aa75-dc91839fdd0d)

How does bidding work
- User submits a bid
- New bid's price must be higher than current bid (Read)
- New bid replaces the current bid (Write)
![image](https://github.com/user-attachments/assets/a12d3889-53a5-43ce-a645-26b6e92306b9)

Scalability: Issues with concurrent bidding system
![image](https://github.com/user-attachments/assets/177de970-40e1-4565-9444-c552b32bbe28)

Scalability: Concurrency
- ConcurrentDictionary
   - Key: AuctionId 
   - Value: Semaphore
- Each AuctionId is assigned to 1 semaphore
- Store the locks and provide row based locks
- Other auctions are not locked
![image](https://github.com/user-attachments/assets/474161bb-e929-41b8-9c5e-d11f9873c378)

Scalability: Concurrency
- Additionally, need to process the lifecycle of the semaphore 
   - Ebay - 1.7 billion active listings
- If auction is not in use, dispose semaphore 
   - Else Dictionary will contain too many semaphore 
- Problem is similar to reader and writer problem
   - If bid is in use, reader +1 
   - If bid is not in use, reader -1
   - If reader == 0 (write)
      - Dispose semaphore

![image](https://github.com/user-attachments/assets/b8ce8266-fc61-463d-bf74-1b4e13633459)

Scalability: Consistent Hashing
- In order to scale the bidding service in parallel. Consistent Hashing needs to be implemented to load balance bid request based on the auctionId
   - Uses a ring-based structure to assign nodes and data based on hash values
   - Bid requests will be hashed and placed on the ring, and sent to the nearest server on the ring structure
- This ensures that no 2 of the bid request with the same auctionId is processed at the same time to ensure concurrency
![image](https://github.com/user-attachments/assets/b488b09a-8db3-4823-96cb-9d100582247b)

Scalability (In-Memory Database / Caching)
- Calling DB over a network is expensive
- Use ConcurrentDictionary to cache bids
- MS-SQL as a persistent storage
   - Primary Source of Truth
   - Strong Consistency
- Decorator and Cache-Aside Design Pattern was used for implementation
![image](https://github.com/user-attachments/assets/352f631b-e4b5-4fa6-ad1d-468c851635d7)
![image](https://github.com/user-attachments/assets/b404196f-e55d-4dd0-a99f-90e1d2bc7605)

Real-Time Bidding Updates (Broadcast Real-Time Communication)
- Real-Time bidding update is implemented using SignalR and event-driven design
   - BidUpdate Request send from 1 client to bidding microservice
   - Bidding service publishes the event to the event bus
   - Bidding Update service then consumes the event from the event bus
   - Bidding Update service then broadcast bid updates to clients based on auctionId (topic)
![image](https://github.com/user-attachments/assets/554ec7da-15a3-4af7-8e62-efe02763d631)

CQRS + Mediator Pattern
- Logical Separation of read and write
   - Implemented in conjunction with the Vertical Slicing Architecture
   - Auction Service
      - GetAuction
      - CreateAuction
- Physical Separation of read and write
   - Implemented as a separate microservice with separate DB for optimization
   - Bidding Service and Bidding Update Service
   - Bidding service 
      - Source of truth (WriteDB)
   - Bidding update service
      - Read Projections (ReadDB)
![image](https://github.com/user-attachments/assets/59aa23ef-aeb0-4285-a76d-88e63b7c209a)

Asynchronous communication
- Domain Events are adapted to Integration Events
- Event handlers publish integration event into message queues
   - RabbitMQ
   - MassTransit
- Publish and Consume Integration Events using Pub / Sub Pattern
![image](https://github.com/user-attachments/assets/d6e84098-4bdf-4a25-83a3-28dbee5b7da3)

Job Scheduler Service: Hangfire
- Background Jobs & Workers Service
- Consumes the AuctionCreatedEvent
- Uses the EndingDate of the auction to schedule jobs
   - Publish the AuctionEndedEvent
![image](https://github.com/user-attachments/assets/bf6f4bda-321e-46f4-85ca-36079f0cf3f4)
![image](https://github.com/user-attachments/assets/27fa3d16-8265-46f9-9d1c-f15c5700a472)

Distributed Caching - Redis
- High Read (132 Million active buyers)
- Cache time consuming operation like the O(n) of 
   - getAuctions, 
   - getAuctionByCategory
- Allows parallel services to have access to a distributed cache for faster reads
- Use Decorator and Cache-Aside Design Pattern for implementation
![image](https://github.com/user-attachments/assets/5ea1e6ad-9f28-4285-be6d-9fa309fc2ef9)
![image](https://github.com/user-attachments/assets/427bf395-c79c-491e-a650-8aaf5d0386cd)

Cross Cutting Concerns
- Mediator design pattern
   - Validation Pipeline behavior
   - Logging Pipeline behavior
- Global Handling Exceptions
- Health Check
   - Check if the current service is healthy
   - GET: {{service-url}}/health
![image](https://github.com/user-attachments/assets/078c31d9-0d63-4818-93b1-7aab717864cc)
![image](https://github.com/user-attachments/assets/829fa8d3-462f-4497-86d1-e4dd5257a725)
![image](https://github.com/user-attachments/assets/deb493d2-9c6b-4329-942e-b6ba9dfaf554)








