package ie.mtu.property_rental.Controller;

import ie.mtu.property_rental.Entities.Properties;
import ie.mtu.property_rental.Services.PropertiesService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;

// Annotation
@RestController
@CrossOrigin(origins = "http://localhost:4200")
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
    public List<Properties> fetchPropertiesList()
    {
        return propertiesService.fetchPropertiesList();
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
