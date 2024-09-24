FROM openjdk:21
# Expose the application's port (adjust as needed)
ARG PORT=8085
EXPOSE 8085


# Copy your application JAR to the image
COPY target/*.jar rental-service.jar

# Set the command to run your application, specifying the main class
CMD ["java", "-jar", "rental-service.jar"]
