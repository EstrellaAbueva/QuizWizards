import React, { useEffect, useRef, useState } from "react";
import { NavLink } from "react-router-dom";
import cn from "classnames";

import GLOBALS from "app-globals";
import {
  Card,
  Container,
  CreateAction,
  Icon,
  IconButton,
  IconLink,
} from "components";
import { actionTypes, iconButtonTypes } from "components/constants";
import { ADMIN_NAV_LINKS, ADMIN_ROUTES } from "screen-wrappers/Admin/constants";
import { useOnClickOutside } from "hooks";
import Logo from "../../static/images/logo.svg";

import styles from "./styles.module.scss";

const CREATE_LINKS = [
  {
    name: "Create Quiz",
    icon: "school",
    action: ADMIN_ROUTES.CREATE_QUIZ,
    colorName: GLOBALS.COLOR_NAMES.BLUE,
    type: actionTypes.LINK,
  },
  {
    name: "Create Topic",
    icon: "menu_book",
    action: ADMIN_ROUTES.CREATE_TOPIC,
    colorName: GLOBALS.COLOR_NAMES.YELLOW,
    type: actionTypes.LINK,
  },
  {
    name: "Create Question",
    icon: "help",
    action: ADMIN_ROUTES.CREATE_QUESTION,
    colorName: GLOBALS.COLOR_NAMES.NEUTRAL,
    type: actionTypes.LINK,
  },
];

const Sidebar = () => {
  const [isCreateDropdownOpen, setIsCreateDropdownOpen] = useState(false);

  const containerRef = useRef();

  useEffect(() => {
    setIsCreateDropdownOpen(false);
  }, [location.pathname]);

  useOnClickOutside(containerRef, () => {
    setIsCreateDropdownOpen(false);
  });

  const onClickCreate = () => {
    setIsCreateDropdownOpen(!isCreateDropdownOpen);
  };

  return (
    <nav ref={containerRef} className={styles.Sidebar}>
      <Container className={styles.Sidebar_container}>
        <div className={styles.Sidebar_links}>
          <img
            className={styles.Sidebar_logo}
            src={Logo}
            alt="logo"
            width={36}
          />
          <div className={cn(styles.Sidebar_links, styles.Sidebar_links_admin)}>
            {ADMIN_NAV_LINKS.map((link) => (
              <NavLink
                to={link.link}
                className={styles.Sidebar_links_link}
                key={link.id}
                activeClassName={styles.Sidebar_links_link___active}
                exact
              >
                <Icon
                  className={styles.Sidebar_links_link_icon}
                  icon={link.icon}
                />
              </NavLink>
            ))}
          </div>
        </div>
        <div className={styles.Sidebar_bottom}>
          <IconButton
            className={styles.Create}
            icon="add_circle"
            type={iconButtonTypes.OUTLINE.LG}
            onClick={onClickCreate}
          />
          {isCreateDropdownOpen && (
            <Card className={styles.Create_dropdown}>
              {CREATE_LINKS.map((action) => (
                <CreateAction key={action.name} {...action} />
              ))}
            </Card>
          )}

          <IconLink
            className={styles.Logout}
            icon="logout"
            type={iconButtonTypes.OUTLINE.MD}
            to="/logout"
          />
        </div>
      </Container>
    </nav>
  );
};

export default Sidebar;
