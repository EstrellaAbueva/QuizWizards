import { useEffect, useState } from "react";
import { TakersService } from "services";

const useTaker = (takerId) => {
  const [isLoading, setIsLoading] = useState(true);
  const [taker, setTaker] = useState(null);

  useEffect(() => {
    const getTaker = async () => {
      const { data: getTakerResponse } = await TakersService.retrieveById(
        takerId
      );

      if (getTakerResponse) {
        setTaker(getTakerResponse);
      }

      setIsLoading(false);
    };

    getTaker();
  }, [takerId]);

  return { isLoading, taker };
};

export default useTaker;
