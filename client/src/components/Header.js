import React, { useEffect, useState } from 'react';
import { NavLink as RRNavLink, useNavigate } from "react-router-dom";
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink
} from 'reactstrap';
import { logout } from '../modules/authManager';

export default function Header({ isLoggedIn }) {
    const [isOpen, setIsOpen] = useState(false);
    
    const nav = useNavigate()
  useEffect( () => {
  
  },[])
  
  const toggle = () => setIsOpen(!isOpen);

  return (
      <div>
          <Navbar color="light" light expand="md">
              <NavbarBrand tag={RRNavLink} to="/">
                  Shoe Collection
              </NavbarBrand>
              <NavbarToggler onClick={toggle} />
              <Collapse isOpen={isOpen} navbar>
                  <Nav className="mr-auto" navbar>
                      {/* When isLoggedIn === true, we will render the Home link */}
                      {isLoggedIn && (
                          <>
                              <NavItem>
                                  <NavLink tag={RRNavLink} to="/">
                                      Home
                                  </NavLink>
                              </NavItem>
                              <NavItem>
                                  <NavLink tag={RRNavLink} to="/brand">
                                      Brand
                                  </NavLink>
                              </NavItem>
                              <NavItem>
                                  <NavLink tag={RRNavLink} to="/style">
                                      Style
                                  </NavLink>
                              </NavItem>
                              <NavItem>
                                  <NavLink tag={RRNavLink} to="/shoe">
                                      All Shoes
                                  </NavLink>
                              </NavItem>
                              <NavItem>
                                  <NavLink tag={RRNavLink} to="/shoe/myshoes">
                                      My Shoes
                                  </NavLink>
                              </NavItem>
                              
                              <NavItem>
                                  <NavLink tag={RRNavLink} to="/myfavorites">
                                      My Favorite Shoes
                                  </NavLink>
                              </NavItem>
                          </>
                      )}
                  </Nav>
                  <Nav navbar>
                      {isLoggedIn && (
                          <>
                              <NavItem>
                                  <a
                                      aria-current="page"
                                      className="nav-link"
                                      style={{ cursor: "pointer" }}
                                      onClick={() => logout(nav("/login"))}
                                  >
                                      Logout
                                  </a>
                              </NavItem>
                          </>
                      )}
                      {!isLoggedIn && (
                          <>
                              <NavItem>
                                  <NavLink tag={RRNavLink} to="/login">
                                      Login
                                  </NavLink>
                              </NavItem>
                              <NavItem>
                                  <NavLink tag={RRNavLink} to="/register">
                                      Register
                                  </NavLink>
                              </NavItem>
                          </>
                      )}
                  </Nav>
              </Collapse>
          </Navbar>
      </div>
  )
}
