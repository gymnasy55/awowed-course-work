import React from 'react'
import {Form} from "react-bootstrap";

export const Register = () => {
  return (
    <>
      <Form>
        <Form.Group controlId="formBasicEmail">
          <Form.Label>Введіть email</Form.Label>
          <Form.Control type="email" placeholder="Введіть email" />
        </Form.Group>

        <Form.Group controlId="formBasicPassword">
          <Form.Label>Пароль</Form.Label>
          <Form.Control type="password" placeholder="Пароль" />
        </Form.Group>
        <Form.Group controlId="formRepeatPassword">
          <Form.Label>Підтвердіть пароль</Form.Label>
          <Form.Control type="password" placeholder="Підтвердіть пароль" />
        </Form.Group>
      </Form>
    </>
  )
}