# SwipedMicroservices
- Swiped! is a real-time e-bidding system that provide real-time bid update to clients.

System Design
![image](https://github.com/user-attachments/assets/33910cb9-3c22-403c-aa75-dc91839fdd0d)

How does bidding work
- User submits a bid
- New bid's price must be higher than current bid (Read)
- New bid replaces the current bid (Write)
![image](https://github.com/user-attachments/assets/a12d3889-53a5-43ce-a645-26b6e92306b9)

Scalability: Issues with concurrent bidding system
![image](https://github.com/user-attachments/assets/177de970-40e1-4565-9444-c552b32bbe28)


