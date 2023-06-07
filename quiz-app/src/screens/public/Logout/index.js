import { useContext, useEffect } from "react";
import { TakerContext } from "context";

const Logout = () => {
  const { loginRestart } = useContext(TakerContext);

  useEffect(() => {
    loginRestart();
  }, []);

  return null;
};

export default Logout;
