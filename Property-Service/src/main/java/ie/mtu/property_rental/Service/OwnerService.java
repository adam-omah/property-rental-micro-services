package ie.mtu.property_rental.Service;


import java.util.List;
import java.util.Objects;

import ie.mtu.property_rental.Entities.Owner;
import ie.mtu.property_rental.Repositories.OwnerRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

// Annotation
@Service

// Class
public class OwnerService {

    @Autowired
    private OwnerRepo ownerRepository;

    // Save operation
    public Owner saveOwner(Owner owner)
    {
        return ownerRepository.save(owner);
    }

    // Read All operation
    public List<Owner> fetchOwnerList()
    {
        return (List<Owner>)
                ownerRepository.findAll();
    }

    // Update operation
    public Owner
    updateOwner(Owner owner,
                Long ownerId)
    {
        Owner ownerDB = ownerRepository.findById(ownerId)
                .get();

        if (Objects.nonNull(owner.getOwnerName())
                && !"".equalsIgnoreCase(
                owner.getOwnerName())) {
            ownerDB.setOwnerName(
                    owner.getOwnerName());
        }

        if (Objects.nonNull(
                owner.getOwnerAddress())
                && !"".equalsIgnoreCase(
                owner.getOwnerAddress())) {
            ownerDB.setOwnerAddress(
                    owner.getOwnerAddress());
        }

        if (Objects.nonNull(owner.getOwnerEmail())
                && !"".equalsIgnoreCase(
                owner.getOwnerEmail())) {
            ownerDB.setOwnerEmail(
                    owner.getOwnerEmail());
        }

        if (Objects.nonNull(owner.getOwnerPhone())
                && !"".equalsIgnoreCase(
                owner.getOwnerPhone())){
            ownerDB.setOwnerPhone(
                    owner.getOwnerPhone());
        }

        if (Objects.nonNull(owner.getOwnerIBAN())
                && !"".equalsIgnoreCase(
                owner.getOwnerIBAN())){
            ownerDB.setOwnerIBAN(
                    owner.getOwnerIBAN());
        }

        return ownerRepository.save(ownerDB);
    }

    // Delete operation
    public void deleteOwnerById(Long ownerId)
    {
        ownerRepository.deleteById(ownerId);
    }
}