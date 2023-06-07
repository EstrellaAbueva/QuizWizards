import { useState } from "react";

import { TakersService } from "../../services";

const useUpdateTaker = () => {
  const [isUpdating, setIsUpdating] = useState(false);

  const updateTaker = async (takerId, updatedTaker) => {
    setIsUpdating(true);

    let responseCode;
    let responseData;
    const errors = {};

    try {
      const response = await TakersService.update(takerId, updatedTaker);

      responseCode = response.status;
    } catch (error) {
      responseData = error.response.data;
      responseCode = error.response.status;

      if (responseCode === 400) {
        if (responseData.errors.Name)
          errors.name = "Name must be first name and last name";
        if (responseData.errors.Address)
          errors.address = "Maximum characters is 50";
        if (responseData.errors.Email) errors.email = "Invalid email address.";
        if (responseData.errors.Username)
          errors.username = "Maximum characters is 50";
        if (responseData.errors.Password)
          errors.password = "Maximum characters is 50";
      }
    }

    setIsUpdating(false);

    return { responseCode, errors };
  };

  return { isUpdating, updateTaker };
};

export default useUpdateTaker;
