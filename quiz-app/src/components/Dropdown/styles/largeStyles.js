import GLOBALS from "../../../app-globals";

export const largeStylesLight = {
  control: (base) => ({
    ...base,
    border: "none",
    boxShadow: null,
    padding: "4px 8px",
    borderRadius: "8px",

    "&:hover": {
      backgroundColor: GLOBALS.COLOR_HEX_CODES.NEUTRAL["100"],
    },
  }),
  placeholder: (base, state) => ({
    ...base,
    display:
      state.isFocused || state.isSelected || state.selectProps.inputValue
        ? "none"
        : "block",
    margin: "0",
  }),
  valueContainer: (base) => ({
    ...base,
    padding: "0",
    fontFamily: "Montserrat, sans-serif",
    fontSize: "2rem",
    fontWeight: "700",
    lineHeight: "1.5",
    overflow: "initial",
    color: GLOBALS.COLOR_HEX_CODES.NEUTRAL["700"],
  }),
  dropdownIndicator: (base) => ({
    ...base,
    color: GLOBALS.COLOR_HEX_CODES.NEUTRAL["500"],
    padding: "0",
    svg: {
      height: "16px",
      width: "16px",
    },
  }),
  clearIndicator: (base) => ({
    ...base,
    padding: "0 8px",
    svg: {
      height: "16px",
      width: "16px",
    },
  }),
  multiValue: (base) => ({
    ...base,
    borderRadius: "16px",
    margin: "0 2px",
    padding: "0 4px",
    backgroundColor: GLOBALS.COLOR_HEX_CODES.NEUTRAL["0"],
    fontWeight: "600",
  }),
  noOptionsMessage: (base) => ({
    ...base,
    fontFamily: "Inter, sans-serif",
    fontSize: "1rem",
    lineHeight: "1.5",
    color: GLOBALS.COLOR_HEX_CODES.NEUTRAL["700"],
  }),
  groupHeading: (base) => ({
    ...base,
    fontFamily: "Inter, sans-serif",
    fontSize: "0.75rem",
    lineHeight: "1.5",
    color: GLOBALS.COLOR_HEX_CODES.NEUTRAL["400"],
  }),
  input: (base) => ({
    ...base,
    margin: "0",
    padding: "0",
    fontWeight: "700",
  }),
  option: (base, state) => ({
    ...base,
    backgroundColor:
      state.isFocused || state.isSelected
        ? GLOBALS.COLOR_HEX_CODES.NEUTRAL["0"]
        : null,
    color: GLOBALS.COLOR_HEX_CODES.NEUTRAL["700"],
    opacity: "1",
    fontFamily: "Inter, sans-serif",
    fontSize: "1rem",
    lineHeight: "1.5",
    overflow: "initial",
  }),
};
