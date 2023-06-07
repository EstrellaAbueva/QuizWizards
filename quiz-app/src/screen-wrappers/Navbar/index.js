import React, { useEffect, useRef, useState } from "react";
import { NavLink } from "react-router-dom";

import GLOBALS from "app-globals";
import { Card, Container, CreateAction, IconButton } from "components";
import { actionTypes, iconButtonTypes } from "components/constants";
import { TAKER_ROUTES } from "screen-wrappers/Taker/constants";
import { useOnClickOutside } from "hooks";
import Logo from "../../static/images/logo-name.svg";

import styles from "./styles.module.scss";

const TAKER_ACCOUNT_LINKS = [
  {
    name: "Settings",
    icon: "settings",
    action: TAKER_ROUTES.EDIT_PROFILE,
    colorName: GLOBALS.COLOR_NAMES.NEUTRAL,
    type: actionTypes.LINK,
  },
  {
    name: "Logout",
    icon: "logout",
    action: "/logout",
    colorName: GLOBALS.COLOR_NAMES.NEUTRAL,
    type: actionTypes.LINK,
  },
];

const Navbar = () => {
  const [isAccountDropdownOpen, setIsAccountDropdownOpen] = useState(false);

  const containerRef = useRef();

  useEffect(() => {
    setIsAccountDropdownOpen(false);
  }, [location.pathname]);

  useOnClickOutside(containerRef, () => {
    setIsAccountDropdownOpen(false);
  });

  const onClickAccount = () => {
    setIsAccountDropdownOpen(!isAccountDropdownOpen);
  };

  return (
    <nav ref={containerRef} className={styles.Navbar}>
      <Container className={styles.Navbar_container}>
        <NavLink className={styles.Navbar_logo} to={TAKER_ROUTES.HOME}>
          <img src={Logo} alt="Quiz Wizard" width={180} />
        </NavLink>

        <div className={styles.Navbar_links}>
          <IconButton
            icon="account_circle"
            type={iconButtonTypes.OUTLINE.LG}
            className={styles.Navbar_links_iconButton}
            onClick={onClickAccount}
          />
          {isAccountDropdownOpen && (
            <Card className={styles.Account_dropdown}>
              {TAKER_ACCOUNT_LINKS.map((action) => (
                <CreateAction key={action.name} {...action} />
              ))}
            </Card>
          )}
        </div>
      </Container>
    </nav>
  );
};

export default Navbar;
