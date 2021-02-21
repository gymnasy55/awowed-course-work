import React, {useState} from 'react'
import {Navbar, Nav, NavDropdown, Form, FormControl, Button, Modal} from 'react-bootstrap'
import {LinkContainer} from 'react-router-bootstrap'
import {Register} from "./Register";
import {Login} from "./Login";

export const MyNavbar = () => {
  const [registerShow, setRegisterShow] = useState(false)
  const [loginShow, setLoginShow] = useState(false)

  const registerHandlerClose = () => setRegisterShow(false);
  const registerHandlerShow = () => setRegisterShow(true);

  const loginHandlerClose = () => setLoginShow(false);
  const loginHandlerShow = () => setLoginShow(true);


  return (
    <Navbar expand="lg" bg="dark" variant="dark">
      <Navbar.Brand href="/">Ювелірний магазин</Navbar.Brand>
      <Navbar.Toggle aria-controls="basic-navbar-nav" />
      <Navbar.Collapse id="basic-navbar-nav">
        <Nav className="mr-auto">
          <LinkContainer to="/">
            <Nav.Link>Головна</Nav.Link>
          </LinkContainer>
          <LinkContainer to="/about">
            <Nav.Link>Контакти</Nav.Link>
          </LinkContainer>
          <NavDropdown title="Дії" id="basic-nav-dropdown">
            <NavDropdown.Item onClick={registerHandlerShow}>Зареєструватися</NavDropdown.Item>
            <Modal
              show={registerShow}
              onHide={registerHandlerClose}
              backdrop="static"
              keyboard={false}
            >
              <Modal.Header closeButton>
                <Modal.Title>Реєстрація</Modal.Title>
              </Modal.Header>
              <Modal.Body>
                <Register />
              </Modal.Body>
              <Modal.Footer>
                <Button variant="secondary" onClick={registerHandlerClose}>
                  Закрити
                </Button>
                <Button variant="primary" onClick={registerHandlerClose}>Зареєструвати</Button>
              </Modal.Footer>
            </Modal>
            <NavDropdown.Divider />
            <NavDropdown.Item onClick={loginHandlerShow}>Увійти</NavDropdown.Item>
            <Modal
              show={loginShow}
              onHide={loginHandlerClose}
              backdrop="static"
              keyboard={false}
            >
              <Modal.Header closeButton>
                <Modal.Title>Увійти</Modal.Title>
              </Modal.Header>
              <Modal.Body>
                <Login />
              </Modal.Body>
              <Modal.Footer>
                <Button variant="secondary" onClick={loginHandlerClose}>
                  Закрити
                </Button>
                <Button variant="primary" onClick={loginHandlerClose}>Увійти</Button>
              </Modal.Footer>
            </Modal>
          </NavDropdown>
        </Nav>
        <Form inline>
          <FormControl type="text" placeholder="Пошук" className="mr-sm-2" />
          <Button variant="success">Пошук</Button>
        </Form>
      </Navbar.Collapse>
    </Navbar>
  )
}