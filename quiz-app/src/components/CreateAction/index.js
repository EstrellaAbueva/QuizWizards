import React from "react";

import PropTypes from "prop-types";
import cn from "classnames";
import { Link } from "react-router-dom";

import GLOBALS from "app-globals";
import { Icon, Text } from "components";

import { actionTypes, textTypes } from "components/constants";

import styles from "./styles.module.scss";

const CreateAction = ({ colorName, icon, name, description, action, type }) => {
  const contentJsx = (
    <>
      <div className={cn(styles.Name, styles[`Name___${colorName}`])}>
        <Icon className={styles.Name_icon} icon={icon} />
        <Text className={styles.Name_text} type={textTypes.DATA.MD}>
          {name}
        </Text>
      </div>
      <Text
        colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["400"]}
        type={textTypes.BODY.XS}
      >
        {description}
      </Text>
    </>
  );

  return type === actionTypes.LINK ? (
    <Link className={styles.Action} to={action}>
      {contentJsx}
    </Link>
  ) : (
    <button className={styles.Action} onClick={action}>
      {contentJsx}
    </button>
  );
};

CreateAction.propTypes = {
  colorName: PropTypes.string,
  icon: PropTypes.string,
  name: PropTypes.string,
  description: PropTypes.string,
  action: PropTypes.oneOf(PropTypes.string, PropTypes.func),
  type: PropTypes.string,
};

export default CreateAction;
