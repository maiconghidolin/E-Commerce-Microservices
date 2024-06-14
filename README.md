# E-Commerce-Microservices
The project consists of several microservices (made in C#, Python, Go and Typescript), each responsible for a specific domain of the e-commerce platform. 

RabbitMQ will be used for inter-service communication and orchestration, while Cassandra and MongoDB will be used for distributed data storage.

## Microservices

### User Service

**Responsibilities:** Handles user registration, authentication, and profile management.

**Data Stored in Cassandra:** User profiles, authentication tokens, and user settings.

### Catalog Service

**Responsibilities:** Manages product catalog, including adding new products, updating product details, and listing products.

**Data Stored in Cassandra:** Product details.

### Order Service

**Responsibilities:** Manages order creation, order status updates, and order history.

**Data Stored in Cassandra:** Orders, order items, and order statuses.

### Payment Service

**Responsibilities:** Processes payments, handles payment status updates, and manages payment methods.

**Data Stored in Cassandra:** Payment transactions, payment methods, and payment statuses.

### Shipping Service

**Responsibilities:** Manages shipping options, shipping status updates, and tracking information.

**Data Stored in Cassandra:** Shipping details, tracking information, and shipping statuses.

### Notification Service

**Responsibilities:** Sends notifications (email, SMS, push notifications) related to user actions, order status changes, etc.

**Data Stored in MongoDB:** Notification history.

### Webhooks Service

**Responsibilities:** Configures and sends data to the webhooks.

### Logging Service

**Responsibilities:** Sends logs (info, error, etc) to Elastic search to show in Kibana.

## Communication Flow

### User Registration:

- User Service receives a registration request.
- User Service sends a message to the Notification Service via RabbitMQ to send a welcome email.
- User Service stores the new user information in Cassandra.

### Product Addition:

- Admin adds a new product via the Catalog Service.
- Product Service updates the product catalog in Cassandra.
- Product Service sends a message to the Notification Service to notify users about the new product.

### Order Placement:

- User places an order via the Order Service.
- Order Service validates the order and sends a message to the Payment Service to process the payment.
- Upon successful payment, Payment Service sends a message back to the Order Service and a message to the Shipping Service to initiate shipping.
- Order Service updates the order status in Cassandra.
- Notification Service sends order confirmation and shipping information to the user.

### Payment Processing:

- Payment Service processes the payment and updates the transaction details in Cassandra.
- Payment Service sends messages to both Order Service and Notification Service to update order status and notify the user.

### Shipping Updates:

- Shipping Service updates the shipping status as the order is processed.
- Shipping Service sends updates to the Order Service and Notification Service.
- Order Service updates the order status in Cassandra.
- Notification Service sends shipping status updates to the user.

## Technologies and Tools

- **.NET 8, C#, Python, Typescript and GO:** For implementing microservices.

- **RabbitMQ:** For message queuing and inter-service communication.

- **Cassandra and MongoDB:** For distributed data storage.

- **Elastic Search and Kibana:** For logging.

- **Docker:** To containerize the microservices for easy deployment.

- **Swagger:** For API documentation.

