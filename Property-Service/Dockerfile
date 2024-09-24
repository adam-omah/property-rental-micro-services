FROM openjdk:21
# Expose the application's port (adjust as needed)
ARG PORT=8084
EXPOSE 8084


# Copy your application JAR to the image
COPY target/*.jar property-service.jar

# Set the command to run your application, specifying the main class
CMD ["java", "-jar", "property-service.jar"]
