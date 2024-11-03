package ie.mtu.property_rental.Services;


import java.util.List;
import java.util.Optional;

import ie.mtu.property_rental.Entities.Properties;
import ie.mtu.property_rental.Repositories.PropertiesRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

// Annotation
@Service

// Class
public class PropertiesService {

    @Autowired
    private PropertiesRepo propertiesRepository;

    // Save operation
    public Properties saveProperties(Properties properties)
    {
        return propertiesRepository.save(properties);
    }
    // Read By ID operation
    public Optional<Properties> fetchPropertyById(Long propertiesId) {
        return propertiesRepository.findById(propertiesId);
    }

    // Read All operation
    public List<Properties> fetchPropertiesList(String status) {
        if (status != null && !status.isEmpty()) {
            // Filter based on the provided status
            return propertiesRepository.findByStatus(status);
        } else {
            // Return all properties if no status is specified
            return propertiesRepository.findAll();
        }
    }


    public Properties updateProperties(Properties properties, Long propertiesId) {
        Properties propertiesDB = propertiesRepository.findById(propertiesId)
                .orElseThrow(() -> new IllegalArgumentException("Properties not found with ID: " + propertiesId));

        Optional.ofNullable(properties.getPropertyName())
                .filter(name -> !name.trim().isEmpty())
                .ifPresent(propertiesDB::setPropertyName);

        Optional.ofNullable(properties.getRentalValue())
                .ifPresent(propertiesDB::setRentalValue);

        Optional.ofNullable(properties.getPropertyType())
                .filter(type -> !type.trim().isEmpty())
                .ifPresent(propertiesDB::setPropertyType);

        Optional.ofNullable(properties.getPropertyDescription())
                .filter(description -> !description.trim().isEmpty())
                .ifPresent(propertiesDB::setPropertyDescription);

        Optional.ofNullable(properties.getBedrooms())
                .ifPresent(propertiesDB::setBedrooms);

        Optional.ofNullable(properties.getBathrooms())
                .ifPresent(propertiesDB::setBathrooms);

        Optional.ofNullable(properties.getParkingSpaces())
                .ifPresent(propertiesDB::setParkingSpaces);

        Optional.ofNullable(properties.getFeatures())
                .filter(features -> !features.trim().isEmpty())
                .ifPresent(propertiesDB::setFeatures);

        return propertiesRepository.save(propertiesDB);
    }

    // Delete operation
    public void deletePropertiesById(Long propertiesId)
    {
        propertiesRepository.deleteById(propertiesId);
    }
}