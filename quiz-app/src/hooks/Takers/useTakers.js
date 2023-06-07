import { useState, useEffect } from "react";

import { TakersService } from "services";

const useTakers = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [takers, setTakers] = useState([]);

  useEffect(() => {
    const getTakers = async () => {
      const { data: getTakerResponse } = await TakersService.list();

      if (getTakerResponse) {
        setTakers(getTakerResponse);
      }

      setIsLoading(false);
    };

    getTakers();
  }, []);

  return { isLoading, takers };
};

export default useTakers;
