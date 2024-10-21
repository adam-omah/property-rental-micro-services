FROM nginx:alpine

# Copy the built Angular application from the correct directory
COPY ./dist/property-rental-ui/browser /usr/share/nginx/html

# Replace the default Nginx configuration with custom settings
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Expose port 4200 for the application
EXPOSE 4200
