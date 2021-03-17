import React from 'react'
import {Container, Row} from "react-bootstrap";
import {Popular} from "../../components/Popular/Popular";

export const HomePage = () => {
    return (
        <>
            <Container>
                <Popular />
            </Container>
        </>
    )
}