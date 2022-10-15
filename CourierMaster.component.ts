import { Component, OnInit,TemplateRef } from '@angular/core';
import { Globalconstants } from "./../../../Helper/globalconstants";
import { OnlineExamServiceService } from "./../../../Services/online-exam-service.service";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import { FormGroup,FormControl, FormBuilder, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import swal from "sweetalert2";
export enum SelectionType {
  single = "single",
  multi = "multi",
  multiClick = "multiClick",
  cell = "cell",
  checkbox = "checkbox",
}
@Component({
  selector: 'app-courier-master',
  templateUrl: './CourierMaster.component.html',
  styleUrls: ['./CourierMaster.component.scss']
})

export class CourierMasterComponent implements OnInit {
  CourierList: any;
  AddUserForm: FormGroup;
  _FilteredList: any;
  entries: number = 10;
  selected: any[] = [];
  temp = [];
  activeRow: any;
  SelectionType = SelectionType;
  modalRef: BsModalRef;

  UserList: any;
  _RoleList: any;
  _LocationList:any;
  submitted = false;
  Reset = false;
  sMsg: string = "";
  //RoleList: any;
  _SingleUser: any = [];
  _UserID: any;
  User:any;
  first = 0;
  rows = 10;
  constructor(private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private _onlineExamService: OnlineExamServiceService,
    private _global: Globalconstants,
    public toastr: ToastrService,) { 
    
  }

  ngOnInit(): void {
    this.AddUserForm = this.formBuilder.group({
      ID: [""],
      Courier: new FormControl('', [Validators.required]),
      User_Token: localStorage.getItem('User_Token'),
    });
    this.getCourierList();
  }

  getCourierList() {
    const userToken = this.AddUserForm.get('User_Token').value || localStorage.getItem('User_Token');
    const apiUrl = this._global.baseAPIUrl + "Courier/GetList?user_Token="+userToken;
    this._onlineExamService.getAllData(apiUrl).subscribe((data: {}) => {
      this.CourierList = data;
      this._FilteredList = data;
      this.prepareTableData( this.CourierList,  this._FilteredList);
      //this.itemRows = Array.from(Array(Math.ceil(this.adresseList.length/2)).keys())
    });
  }
  editCourier(template: TemplateRef<any>, value: any) {
      //this.modalRef = this.modalService.show(template);

    this.User ="Edit Courier details";
    const apiUrl =
      this._global.baseAPIUrl +
      "Courier/GetDetails?ID=" +
      value.id +"&user_Token=" + localStorage.getItem('User_Token');
    this._onlineExamService.getAllData(apiUrl).subscribe((data: any) => {
      var that = this;
      that._SingleUser = data;
   //   console.log('data', data);
      this.AddUserForm.patchValue({
        ID:that._SingleUser.ID,
        Courier: that._SingleUser.Courier,
      })
    });
    
    this.modalRef = this.modalService.show(template);
  }

  searchTable($event) {
    // console.log($event.target.value);
  
    let val = $event.target.value;
    if(val == '') {
      this.formattedData = this.immutableFormattedData;
    } else {
      let filteredArr = [];
      const strArr = val.split(',');
      this.formattedData = this.immutableFormattedData.filter(function (d) {
        for (var key in d) {
          strArr.forEach(el => {
            if (d[key] && el!== '' && (d[key]+ '').toLowerCase().indexOf(el.toLowerCase()) !== -1) {
              if (filteredArr.filter(el => el.srNo === d.srNo).length === 0) {
                filteredArr.push(d);
              }
            }
          });
        }
      });
      this.formattedData = filteredArr;
    }
  }
  formattedData: any = [];
  headerList: any;
  immutableFormattedData: any;
  loading: boolean = true;
  prepareTableData(tableData, headerList) {
    let formattedData = [];
   // alert(this.type);
  
  // if (this.type=="Checker" )
  //{
    let tableHeader: any = [
      { field: 'srNo', header: "SR NO", index: 1 },
      { field: 'Courier', header: 'NAME', index: 3 }
  
    ];
   
    tableData.forEach((el, index) => {
      formattedData.push({
        'srNo': parseInt(index + 1),
        'id': el.ID,
        'Courier': el.Courier
      
      });
   
    });
    this.headerList = tableHeader;
  //}
  
    this.immutableFormattedData = JSON.parse(JSON.stringify(formattedData));
    this.formattedData = formattedData;
    this.loading = false;
  
  }
  addUser(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
    this.AddUserForm.patchValue({
      Courier: ''
    })
    this.User ="Create Courier";
  }
  
  OnClose()
  {
    this.modalService.hide(1);
  }
  
  ShowErrormessage(data:any)
  {
    this.toastr.show(
      '<div class="alert-text"</div> <span class="alert-title" data-notify="title">Error ! </span> <span data-notify="message"> '+ data +' </span></div>',
      "",
      {
        timeOut: 3000,
        closeButton: true,
        enableHtml: true,
        tapToDismiss: false,
        titleClass: "alert-title",
        positionClass: "toast-top-center",
        toastClass:
          "ngx-toastr alert alert-dismissible alert-danger alert-notify"
      }
    );
  
  
  }
  ShowMessage(data:any)
  {
    this.toastr.show(
      '<div class="alert-text"</div> <span class="alert-title" data-notify="title">Success ! </span> <span data-notify="message"> '+ data +' </span></div>',
      "",
      {
        timeOut: 3000,
        closeButton: true,
        enableHtml: true,
        tapToDismiss: false,
        titleClass: "alert-title",
        positionClass: "toast-top-center",
        toastClass:
          "ngx-toastr alert alert-dismissible alert-success alert-notify"
      }
    );
  
  
  }
  onSubmit() {
    this.submitted = true; 

    
     if(this.AddUserForm.value.Courier =="") {
      this.ShowErrormessage("Please enter name");
      return;
     }

     

    if(this.AddUserForm.value.User_Token == null) {
      this.AddUserForm.value.User_Token = localStorage.getItem('User_Token');
    }
    if (this.AddUserForm.get('ID').value) {
      const apiUrl = this._global.baseAPIUrl + "Courier/Update";
      this._onlineExamService
        .postData(this.AddUserForm.value, apiUrl)
        // .pipe(first())
        .subscribe((data) => {
          if (data != null) {
           
            this.ShowMessage("Record Updated Successfully.."); 
            this.modalService.hide(1);
            this.OnReset();
            this.getCourierList();
          } else {
            // Open Modal like you have opned in other pages
            //alert("Courier already exists.");
          }
        });
    } else {
      const apiUrl = this._global.baseAPIUrl + "Courier/Create";
      this._onlineExamService
        .postData(this.AddUserForm.value, apiUrl)
        // .pipe(first())
        .subscribe((data) => {
          if (data == 1) {
           this.ShowMessage("Record Saved Successfully.."); 
           
           this.OnReset();
            this.getCourierList();
            this.modalService.hide(1);
          } else {
            this.ShowErrormessage("Courier already exists.");
          }
        });
    }

  }
  deleteCourier(id: any) {

    if (id != localStorage.getItem('UserID'))
{
    swal
      .fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        type: "warning",
        showCancelButton: true,
        buttonsStyling: false,
        confirmButtonClass: "btn btn-danger",
        confirmButtonText: "Yes, delete it!",
        cancelButtonClass: "btn btn-secondary",
      })
      .then((result) => {
        if (result.value) {
          this.AddUserForm.patchValue({
            ID: id.id,
            User_Token: localStorage.getItem('User_Token'),
          });

          const apiUrl = this._global.baseAPIUrl + "Courier/Delete";
          this._onlineExamService
            .postData(this.AddUserForm.value, apiUrl)
            .subscribe((data) => {
              swal.fire({
                title: "Deleted!",
                text: "Courier has been deleted.",
                type: "success",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-primary",
              });
              this.getCourierList();
            });
        }
      });
    }
    else
    {

      this.ShowErrormessage("Your already log in so you can not delete!");
    }
    }
  OnReset() {
    this.Reset = true;
    this.AddUserForm.reset();
    this.User ="Create Courier";
  }


}
