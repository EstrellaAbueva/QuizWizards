import { useState } from "react";
import { TakersService } from "services";

const useRecordAnswer = () => {
  const [isRecording, setIsRecording] = useState(false);

  const recordAnswer = async (answer) => {
    setIsRecording(true);

    let responseCode;

    try {
      const response = await TakersService.recordAnswer(answer);

      responseCode = response.status;
    } catch (error) {
      responseCode = error.response.status;
    }

    setIsRecording(false);

    return { responseCode };
  };

  return { isRecording, recordAnswer };
};

export default useRecordAnswer;
