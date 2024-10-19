# Step 1: Use a Node.js base image
FROM node:14-alpine

# Step 2: Set the working directory inside the container
WORKDIR /app

# Step 3: Copy the package.json and package-lock.json to the container
COPY package.json package-lock.json ./

# Step 4: Install the dependencies
RUN npm install

# Step 5: Copy the rest of your application to the container
COPY . .

# Step 6: Build the React app for production
RUN npm run build

# Step 7: Install a simple HTTP server to serve the built app
RUN npm install -g serve

# Step 8: Expose port 5000 (this is the port the server will use)
EXPOSE 5000

# Step 9: Start the server to serve your app
CMD ["serve", "-s", "build", "-l", "5000"]
