import React from "react";
import ExhausterComponent from "./ExhausterComponent";
class MainPage extends React.Component{
    constructor(props){
        super();
    }
    render(){
        return <div className="page-container">
            <ExhausterComponent></ExhausterComponent>
        </div>
    }
}
export default MainPage;