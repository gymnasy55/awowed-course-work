import React, { useState } from 'react'
import { Button, Card } from "react-bootstrap";
import classes from './PopularItem.module.css'

export const PopularItem = ({title, idx}) => {
  const [hover, setHover] = useState(false);

  const MouseEnterHandler = () => setHover(true)

  const MouseLeaveHandler = () => setHover(false)

  return (
    <div
      className="col-sm-12 col-md-6 col-lg-4 col-xl-3 mb-1"
      onMouseEnter={MouseEnterHandler}
      onMouseLeave={MouseLeaveHandler}
    >
      <Card border={hover ? "primary" : ""} key={idx} className={classes.card}>
        <Card.Img variant="top" src="https://via.placeholder.com/60" />
        <Card.Body>
          <Card.Title>{title}</Card.Title>
          <Card.Text>
            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ad aliquam assumenda
            at deserunt ipsam iusto nesciunt nihil quod sapiente unde.
          </Card.Text>
          <Button variant="primary" className="w-100">Перейти до...</Button>
        </Card.Body>
      </Card>
    </div>
  )
}