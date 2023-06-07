import React from "react";
import cn from "classnames";
import PropTypes from "prop-types";
import styles from "./styles.module.scss";

const Card = React.forwardRef(
  (
    { id, children, className, onClick, onMouseEnter, onMouseLeave, ...rest },
    ref
  ) => {
    const keyPressHandler = (e) => {
      if (e.key === "Enter" || e.key === " ") {
        onClick();
      }
    };

    return (
      <div
        {...rest}
        ref={ref}
        className={cn(styles.Card, className, {
          [styles.Card___clickable]: onClick !== null,
        })}
        id={id}
        role={onClick ? "button" : null}
        tabIndex={onClick ? 0 : null}
        onClick={onClick}
        onKeyPress={onClick ? keyPressHandler : null}
        onMouseEnter={onMouseEnter}
        onMouseLeave={onMouseLeave}
      >
        {children}
      </div>
    );
  }
);

Card.defaultProps = {
  id: null,
  children: null,
  className: null,
  onClick: null,
  onMouseEnter: null,
  onMouseLeave: null,
};

Card.propTypes = {
  id: PropTypes.string,
  children: PropTypes.any,
  className: PropTypes.string,
  onClick: PropTypes.func,
  onMouseEnter: PropTypes.func,
  onMouseLeave: PropTypes.func,
};

Card.displayName = "Card";

export default Card;
