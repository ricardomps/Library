# Library App
## Environment Variables

This project uses environment variables to configure the PostgreSQL database connection and other app settings.

### Creating your `.env` file

1. Copy the example environment file:

```bash
    cp .env.example .env
```

2. Open `.env` with a text editor and update the values accordingly:

## Running the Project with Docker Compose

After configuring your `.env` file, you can start the project using Docker Compose:

```bash
docker compose up --build -d
```

This command will build and start all necessary services defined in the `docker-compose.yml` file. To stop the services run:

```bash
docker compose down
```

## Accessing the Application

Once the services are running, you can access the following URLs in your browser:

- **Main Application:** [http://localhost:3000](http://localhost:3000)
- **Swagger API Docs:** [http://localhost:5000/swagger](http://localhost:5000/swagger)
