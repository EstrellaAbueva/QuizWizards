import React from "react";

import PropTypes from "prop-types";
import { TakerTag, CardLink } from "components";

import styles from "./styles.module.scss";

const TakerCardLink = ({ name, link }) => {
  return (
    <CardLink className={styles.TakerCardLink} to={link}>
      <TakerTag name={name} isAdmin />
    </CardLink>
  );
};

TakerCardLink.propTypes = {
  name: PropTypes.string.isRequired,
  link: PropTypes.string.isRequired,
};

export default TakerCardLink;
