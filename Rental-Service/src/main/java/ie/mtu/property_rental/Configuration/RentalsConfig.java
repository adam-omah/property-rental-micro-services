package ie.mtu.property_rental.Configuration;

import lombok.Getter;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Configuration;

@Configuration
public class RentalsConfig {
    // Static getter
    @Getter
    private static String propertiesServiceUrl; // Static field

    @Value("${spring.user.api}")
    public void setPropertiesServiceUrl(String url) { // Setter method
        RentalsConfig.propertiesServiceUrl = url;
    }
}
