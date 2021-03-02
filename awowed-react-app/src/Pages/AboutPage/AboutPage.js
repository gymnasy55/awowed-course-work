import React, {useEffect, useState} from 'react'
import {Button, Card, Container} from "react-bootstrap";
const {apiPath} = require('../../config.json')

export const AboutPage = () => {
  const [error, setError] = useState(null);
  const [isLoaded, setIsLoaded] = useState(false);
  const [contacts, setContacts] = useState([]);

  useEffect(() => {
    window.fetch(`${apiPath}users`)
      .then(res => res.json())
      .then(
        (result) => {
          setIsLoaded(true);
          setContacts(result);
        },
        // Примечание: важно обрабатывать ошибки именно здесь, а не в блоке catch(),
        // чтобы не перехватывать исключения из ошибок в самих компонентах.
        (error) => {
          setIsLoaded(true);
          setError(error);
        }
      )
  }, [])

  if(error) {
    return <Container><h1>Помилка: {error.message}</h1></Container>
  } else if (!isLoaded) {
    return <Container><h1>Загрузка...</h1></Container>
  } else {
    return (
      <>
        <Container>
          <h1>Контакти</h1>
          <div className="row">
            {contacts.map((contact, idx) => {
              return (
                <div className="col-sm-12 col-md-6 col-lg-4 col-xl-4 mb-1"
                     key={contact.id}>
                  <Card>
                    <Card.Img variant="top" src="https://via.placeholder.com/60"/>
                    <Card.Body>
                      <Card.Title>{contact.name}</Card.Title>
                      <Card.Text>
                        <b>Телефон: </b>{contact.phone}
                        <br/>
                        <b>Email: </b>{contact.email}
                      </Card.Text>
                    </Card.Body>
                  </Card>
                </div>
              )
            })}
          </div>
        </Container>
      </>
    )
  }
}