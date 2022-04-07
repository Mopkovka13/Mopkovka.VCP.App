import React,{Component} from 'react';
import { variables} from './Variables';

export class Volunteer extends Component{
    constructor(props){
        super(props);

        this.state={
            Activitys:[],
            Volunteers:[],
            modalTitle:"",
            VolunteerId:0,
            VolunteerFirstName:"",
            VolunteerLastName:"",
            Activity:"",
            Institute:""
        }
    }
    
    refreshList(){
        fetch(variables.API_URL+'Volunteer')
        .then(response=>response.json())
        .then(data=>{
            this.setState({Volunteers:data});
        })

        fetch(variables.API_URL+'Activity')
        .then(response=>response.json())
        .then(data=>{
            this.setState({Activitys:data});
        })
    }



    componentDidMount(){
        this.refreshList();
    }

    changeVolunteerFirstName =(e)=>{
        this.setState({VolunteerFirstName:e.target.value});
    }
    changeVolunteerLastName =(e)=>{
        this.setState({VolunteerLastName:e.target.value});
    }
    changeActivity =(e)=>{
        this.setState({Activity:e.target.value});
    }
    changeInstitute =(e)=>{
        this.setState({Institute:e.target.value});
    }


    addClick(){
        this.setState({
            modalTitle:"Add Volunteer",
            VolunteerId:0,
            VolunteerFirstName:"",
            VolunteerLastName:"",
            Activity:"",
            Institute:""
        });
    }

    editClick(vol){
        this.setState({
            modalTitle:"Edit Volunteer",
            VolunteerId:vol.VolunteerId,
            VolunteerFirstName:vol.VolunteerFirstName,
            VolunteerLastName:vol.VolunteerLastName,
            Activity:vol.Activity,
            Institute:vol.Institute
        });
    }

    createClick(){
        fetch(variables.API_URL+'Volunteer',{
            method:'POST',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            },
            body:JSON.stringify({
                VolunteerFirstName:this.state.VolunteerFirstName,
                VolunteerLastName:this.state.VolunteerLastName,
                Activity:this.state.Activity,
                Institute:this.state.Institute
            })
        })
        .then(res=>res.json())
        .then((result)=>{
            alert(result);
            this.refreshList();
        },(error)=>{
            alert('Failed');
        })
    }

    deleteClick(id){
        if(window.confirm('Are you sure?')){
        fetch(variables.API_URL+'Volunteer/'+id,{
            method:'DELETE',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            }
        })
        .then(res=>res.json())
        .then((result)=>{
            alert(result);
            this.refreshList();
        },(error)=>{
            alert('Failed');
        })
        }
    }

    updateClick(){
        fetch(variables.API_URL+'Volunteer',{
            method:'PUT',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            },
            body:JSON.stringify({
                VolunteerId:this.state.VolunteerId,
                VolunteerFirstName:this.state.VolunteerFirstName,
                VolunteerLastName:this.state.VolunteerLastName,
                Institute:this.state.Institute,
                Activity:this.state.Activity

            })
        })
        .then(res=>res.json())
        .then((result)=>{
            alert(result);
            this.refreshList();
        },(error)=>{
            alert('Failed');
        })
    }

    render(){
        const{
            Activitys,
            Volunteers,
            modalTitle,
            VolunteerId,
            VolunteerFirstName,
            VolunteerLastName,
            Institute
        }=this.state;
        return(
<div>
    <button type="button"
    className="btn btn-primary m-2 float-end"
    data-bs-toggle="modal"
    data-bs-target="#exampleModal"
    onClick={()=>this.addClick()}>
        Add Volunteer
    </button>
    <table className="table table-striped">
    <thead>
    <tr>
        <th>
            VolunteerId
        </th>
        <th>
            VolunteerFirstName
        </th>
        <th>
            VolunteerLastName
        </th>
        <th>
            Institute
        </th>
        <th>
            Activity
        </th>
    </tr>
    </thead>
    <tbody>
        {Volunteers.map(vol=>
            <tr key={vol.VolunteerId}>
                <td>{vol.VolunteerId}</td>
                <td>{vol.VolunteerFirstName}</td>
                <td>{vol.VolunteerLastName}</td>
                <td>{vol.Institute}</td>
                <td>{vol.Activity}</td>
                <td>
                <button type="button"
                className="btn btn-light mr-1"
                data-bs-toggle="modal"
                data-bs-target="#exampleModal"
                onClick={()=>this.editClick(vol)}>
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                    <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                    <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
                    </svg>
                </button>

                <button type="button"
                className="btn btn-light mr-1"
                onClick={()=>this.deleteClick(vol.VolunteerId)}>
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-trash-fill" viewBox="0 0 16 16">
                    <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z"/>
                    </svg>
                </button>
                </td>
            </tr>
            )}
    </tbody>
    </table>

<div className="modal fade" id="exampleModal" tabIndex="-1" aria-hidden="true">
<div className="modal-dialog modal-lg modal-dialog-centered">
<div className="modal-content">
   <div className="modal-header">
       <h5 className="modal-title">{modalTitle}</h5>
       <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"
       ></button>
   </div>

   <div className="modal-body">
       <div className="bd-highlight">

        <div className="input-group mb-3">
            <span className="input-group-text">VolunteerFirstName</span>
            <input type="text" className="form-control"
            value={VolunteerFirstName}
            onChange={this.changeVolunteerFirstName}/>
        </div>
        <div className="input-group mb-3">
            <span className="input-group-text">VolunteerLastName</span>
            <input type="text" className="form-control"
            value={VolunteerLastName}
            onChange={this.changeVolunteerLastName}/>
        </div>

        <div className="input-group mb-3">
            <span className="input-group-text">Institute</span>
            <input type="text" className="form-control"
            value={Institute}
            onChange={this.changeInstitute}/>
        </div>

        <div className="input-group mb-3">
            <span className="input-group-text">Activity</span>
            <select className="form-select"
            onChange={this.changeActivity}
            value={this.Activity}>
                {Activitys.map(act=><option key={act.ActivityId}>
                    {act.ActivityName}
                </option>)}
            </select>
        </div>

       </div>
       

        {VolunteerId==0?
        <button type="button"
        className="btn btn-primary float-start"
        onClick={()=>this.createClick()}
        >Create</button>
        :null}

        {VolunteerId!=0?
        <button type="button"
        className="btn btn-primary float-start"
        onClick={()=>this.updateClick()}
        >Update</button>
        :null}

   </div>

</div>
</div> 
</div>

</div>
        )
    }
}