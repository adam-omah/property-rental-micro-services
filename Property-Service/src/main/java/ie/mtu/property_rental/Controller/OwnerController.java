package ie.mtu.property_rental.Controller;

import ie.mtu.property_rental.Entities.Owner;
import ie.mtu.property_rental.Services.OwnerService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

// Annotation
@RestController
@CrossOrigin(origins = "http://localhost:4200")
// Class
public class OwnerController {

    @Autowired
    private OwnerService ownerService;

    // Save operation
    @PostMapping("/owners")
    public Owner saveOwner(
            @RequestBody Owner owner)
    {
        return ownerService.saveOwner(owner);
    }

    // Read operation
    @GetMapping("/owners")
    public List<Owner> fetchOwnerList()
    {
        return ownerService.fetchOwnerList();
    }

    // Update operation
    @PutMapping("/owners/{id}")
    public Owner
    updateOwner(@RequestBody Owner owner,
                     @PathVariable("id") Long ownerId)
    {
        return ownerService.updateOwner(
                owner, ownerId);
    }

    // Delete operation
    @DeleteMapping("/owners/{id}")
    public String deleteOwnerById(@PathVariable("id")
                                       Long ownerId)
    {
        ownerService.deleteOwnerById(
                ownerId);
        return "Deleted Successfully";
    }
}
