import pymysql
import pika
import json

# MySQL Connection Configuration
db_connection = pymysql.connect(
    host="localhost",  
    user="User1",
    password="User123",
    database="MachineLearningDB",
    port=3307
)

# RabbitMQ Connection Configuration
rabbitmq_host = "localhost"  
queue_name = "ProductCategorizationQueue"

# Set up RabbitMQ connection and declare the queue
connection = pika.BlockingConnection(pika.ConnectionParameters(host=rabbitmq_host))
channel = connection.channel()
channel.queue_declare(queue=queue_name, durable=True)

try:
    with db_connection.cursor() as cursor:

        cursor.execute("SELECT Id, ProductName, ProductDescription, PredictedCategory, Probability, PredictionDate FROM predictions")
        
        for row in cursor.fetchall():
            message = {
                "id": row[0],
                "productName": row[1],
                "productDescription": row[2],
                "predictedCategory": row[3],
                "probability": row[4],
                "predictionDate": row[5].isoformat() if row[5] else None 
            }

            message_json = json.dumps(message)
            channel.basic_publish(
                exchange='',
                routing_key=queue_name,
                body=message_json,
                properties=pika.BasicProperties(delivery_mode=2)  # make message persistent
            )

            print(f"Sent: {message_json}")

finally:
    # Close connections
    connection.close()
    db_connection.close()
