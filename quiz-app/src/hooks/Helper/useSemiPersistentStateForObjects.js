import { useEffect, useState } from "react";

const useSemiPersistentStateForObjects = (key, initialState) => {
  const [value, setValue] = useState(
    localStorage.getItem(key)
      ? JSON.parse(localStorage.getItem(key))
      : initialState
  );

  useEffect(() => {
    localStorage.setItem(key, JSON.stringify(value));
  }, [value, key]);

  return [value, setValue];
};

export default useSemiPersistentStateForObjects;
