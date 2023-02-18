import React from "react";
import ExgausterComponent from "./ExgausgerComponent";
import '../css/aglomachine.css'
class AglomachineComponent extends React.Component{
    constructor(props){
        super()
        this.props = props;
        console.log(this.props.data)
        
    }
    RenderExgausters(){
        let results = [];
        this.props.data.forEach((element,index) => {
          results.push(<ExgausterComponent key={index} data={element}/>)
        });
        return results
    }
    render(){
        return <div>
            <div className="aglo-head">{this.props.data[0].name}</div>
            <div className="ex-container">{this.RenderExgausters()}</div>
        </div>
    }
}
export default AglomachineComponent;