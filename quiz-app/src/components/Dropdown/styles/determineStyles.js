import dropdownTypes from "../constants/dropdownTypes";

import { formStylesLight  } from "./formStyles";
import { largeStylesLight } from "./largeStyles";
import {
  slimStylesLight,
  slimStylesLightWithWhiteBackground,
} from "./slimStyles";

const determineStyles = (type, hasBackground = false) => {
  switch (type) {
    case dropdownTypes.FORM:
      return formStylesLight;
    case dropdownTypes.SLIM:
      // eslint-disable-next-line no-case-declarations
      const slimLightStyle = hasBackground
        ? slimStylesLightWithWhiteBackground
        : slimStylesLight;

      return slimLightStyle;
    case dropdownTypes.LARGE:
      return largeStylesLight;
    default:
      return slimStylesLight;
  }
};

export default determineStyles;
