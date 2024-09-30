package ie.mtu.property_rental.Controller;

import ie.mtu.property_rental.Entities.Properties;
import ie.mtu.property_rental.Services.PropertiesService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

// Annotation
@RestController

// Class
public class PropertiesController {

    @Autowired
    private PropertiesService propertiesService;

    // Save operation
    @PostMapping("/propertiess")
    public Properties saveProperties(
            @RequestBody Properties properties)
    {
        return propertiesService.saveProperties(properties);
    }

    // Read operation
    @GetMapping("/propertiess")
    public List<Properties> fetchPropertiesList()
    {
        return propertiesService.fetchPropertiesList();
    }

    // Update operation
    @PutMapping("/propertiess/{id}")
    public Properties
    updateProperties(@RequestBody Properties properties,
                @PathVariable("id") Long propertiesId)
    {
        return propertiesService.updateProperties(
                properties, propertiesId);
    }

    // Delete operation
    @DeleteMapping("/propertiess/{id}")
    public String deletePropertiesById(@PathVariable("id")
                                  Long propertiesId)
    {
        propertiesService.deletePropertiesById(
                propertiesId);
        return "Deleted Successfully";
    }
}
