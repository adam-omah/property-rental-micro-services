package ie.mtu.property_rental.Services;


import java.util.List;
import java.util.Optional;

import ie.mtu.property_rental.Entities.Owner;
import ie.mtu.property_rental.Repositories.OwnerRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

// Annotation
@Service
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


    public Owner updateOwner(Owner owner, Long ownerId) {
        Owner ownerDB = ownerRepository.findById(ownerId)
                .orElseThrow(() -> new IllegalArgumentException("Owner not found with ID: " + ownerId));

        Optional.ofNullable(owner.getOwnerName())
                .filter(name -> !name.trim().isEmpty())
                .ifPresent(ownerDB::setOwnerName);

        Optional.ofNullable(owner.getOwnerAddress())
                .filter(address -> !address.trim().isEmpty())
                .ifPresent(ownerDB::setOwnerAddress);

        Optional.ofNullable(owner.getOwnerEmail())
                .filter(email -> !email.trim().isEmpty())
                .ifPresent(ownerDB::setOwnerEmail);

        Optional.ofNullable(owner.getOwnerPhone())
                .filter(phone -> !phone.trim().isEmpty())
                .ifPresent(ownerDB::setOwnerPhone);

        Optional.ofNullable(owner.getOwnerIBAN())
                .filter(iban -> !iban.trim().isEmpty())
                .ifPresent(ownerDB::setOwnerIBAN);

        return ownerRepository.save(ownerDB);
    }

    // Delete operation
    public void deleteOwnerById(Long ownerId)
    {
        ownerRepository.deleteById(ownerId);
    }
}