package ie.mtu.property_rental.Controller;

import ie.mtu.property_rental.Entities.Properties;
import ie.mtu.property_rental.Services.PropertiesService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;

// Annotation
@RestController
@CrossOrigin(origins = {"http://localhost:4200","http://localhost:8085","http://rental-service:8084"})
// Class
public class PropertiesController {

    @Autowired
    private PropertiesService propertiesService;

    // Save operation
    @PostMapping("/properties")
    public Properties saveProperties(
            @RequestBody Properties properties)
    {
        return propertiesService.saveProperties(properties);
    }

    // Read operation
    @GetMapping("/properties")
    public List<Properties> fetchPropertiesList(
            @RequestParam(name = "status", required = false) String status  // Add this parameter
    ) {
        return propertiesService.fetchPropertiesList(status); // Pass status to the service
    }
    @GetMapping("/properties/{id}")
    public Optional<Properties> fetchPropertiesFromID(@PathVariable Long id)
    {
        return propertiesService.fetchPropertyById(id);
    }

    // Update operation
    @PutMapping("/properties/{id}")
    public Properties
    updateProperties(@RequestBody Properties properties,
                @PathVariable("id") Long propertiesId)
    {
        return propertiesService.updateProperties(
                properties, propertiesId);
    }

    // Delete operation
    @DeleteMapping("/properties/{id}")
    public String deletePropertiesById(@PathVariable("id")
                                  Long propertiesId)
    {
        propertiesService.deletePropertiesById(
                propertiesId);
        return "Deleted Successfully";
    }
}
