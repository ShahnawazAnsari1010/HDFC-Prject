import { Globalconstants } from "./../../../Helper/globalconstants";
import { OnlineExamServiceService } from "./../../../Services/online-exam-service.service";
import { Component, OnInit, TemplateRef } from "@angular/core";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import { FormGroup,FormControl, FormBuilder, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import swal from "sweetalert2";
import * as XLSX from 'xlsx';
export enum SelectionType {
  single = "single",
  multi = "multi",
  multiClick = "multiClick",
  cell = "cell",
  checkbox = "checkbox",
}
@Component({
  selector: "app-users",
  templateUrl: "InwardUpload.component.html",
})
export class InwardUploadComponent implements OnInit {
  ExcelData:any;
  entries: number = 10;
  selected: any[] = [];
  temp = [];
  activeRow: any;
  SelectionType = SelectionType;
  modalRef: BsModalRef;

  UserList: any;
  _FilteredList: any;
  _RoleList: any;
  _LocationList:any;
  AddUserForm: FormGroup;
  submitted = false;
  Reset = false;
  //_UserList: any;
  sMsg: string = "";
  //RoleList: any;
  _SingleUser: any = [];
  _UserID: any;
  User:any;
  first = 0;
  rows = 10;
  data:any;
  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private _onlineExamService: OnlineExamServiceService,
    private _global: Globalconstants,
    public toastr: ToastrService,
  ) {  this.data = [
    [2019, 1, '50', '20', '25', '20'],
    [2019, 2, '80', '20', '25', '20'],
    [2019, 3, '120', '20', '25', '20'],  
    [2019, 4, '75', '20', '25', '20'],  
    [2019, 5, '60', '20', '25', '20'],  
    [2019, 6, '80', '20', '25', '20'],  
    [2019, 7, '95', '20', '25', '20'],  
    [2019, 8, '55', '20', '25', '20'],  
    [2019, 9, '45', '20', '25', '20'],  
    [2019, 10, '80', '20', '25', '20'],  
    [2019, 11, '90', '20', '25', '20'],  
    [2019, 12, '110', '20', '25', '20'],      
  ];}
  ngOnInit() {
    
      this.loading=false;
    

  }
  DownloadTemplate(){
  const link = document.createElement('a');
  link.setAttribute('target', '_blank');
  link.setAttribute('href', '_File_Saved_Path');
  link.setAttribute('download', 'file_name.pdf');
  }
  //Newly added code 
    Upload(){
      const apiUrl = this._global.baseAPIUrl + "InwardUpload/Create";
        this._onlineExamService
          .postData(this.ExcelData, apiUrl)
          // .pipe(first())
          .subscribe((data) => {
            
             this.ShowMessage("Record Saved Successfully.."); 
            
          });
    }
    generateExcel() {

      // console.log('called');
      // this._onlineExamService.generateExcel();
     }
  ReadExcel(event:any){
    let file=event.target.files[0];
    let fileReader=new FileReader();
    fileReader.readAsBinaryString(file);
    fileReader.onload=(e)=>{
      var WorkBook=XLSX.read(fileReader.result,{type:'binary'});
      var sheetNames=WorkBook.SheetNames;
      this.ExcelData=XLSX.utils.sheet_to_json(WorkBook.Sheets[sheetNames[0]]);
      this.prepareTableData( this.ExcelData,  this.ExcelData);
      
    } 
  
    }
    


  entriesChange($event) {
    this.entries = $event.target.value;
  }
  filterTable($event) {
  //  console.log($event.target.value);

    let val = $event.target.value;
    this._FilteredList = this.UserList.filter(function (d) {
    //  console.log(d);
      for (var key in d) {
        if (key == "name" || key == "email" || key == "userid" || key == "mobile" || key == "roleName") {
          if (d[key].toLowerCase().indexOf(val.toLowerCase()) !== -1) {
            return true;
          }
        }
      }
      return false;
    });
  }
  onSelect({ selected }) {
    this.selected.splice(0, this.selected.length);
    this.selected.push(...selected);
  }
  onActivate(event) {
    this.activeRow = event.row;
  }

  paginate(e) {
    this.first = e.first;
    this.rows = e.rows;
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
    { field: 'AGEEMENTNO', header: 'AGEEMENTNO', index: 3 },
    { field: 'BARCODE', header: 'BARCODE', index: 2 },
     { field: 'PRODUCTNAME', header: 'PRODUCTNAME', index: 3 },
    { field: 'DISBURSEMENTDATE', header: 'DISBURSEMENTDATE', index: 3 },
    { field: 'SYSTEM', header: 'SYSTEM', index: 3 },
    { field: 'CASESTARTERBRANCH', header: 'CASESTARTERBRANCH', index: 3 },
    { field: 'GENERATEDON', header: 'GENERATEDON', index: 3 },
    { field: 'CPUID', header: 'CPUID', index: 3 },
    { field: 'batch_id', header: 'batch_id', index: 3 },
    { field: 'RepaymentMode', header: 'RepaymentMode', index: 3 },
    { field: 'VendorID', header: 'VendorID', index: 3 },

  ];
 
  tableData.forEach((el, index) => {
    formattedData.push({
      'srNo': parseInt(index + 1),
      'AGEEMENTNO':el.AGEEMENTNO,
      'BARCODE':el.BARCODE,
      'PRODUCTNAME': el.PRODUCTNAME,
      'DISBURSEMENTDATE': el.DISBURSEMENTDATE,
      'SYSTEM': el.SYSTEM,
      'CASESTARTERBRANCH': el.CASESTARTERBRANCH,
      'GENERATEDON': el.GENERATEDON,
      'CPUID':el.CPUID,
      'batch_id':el.batch_id,
      'RepaymentMode':el.RepaymentMode,
      'VendorID':el.VendorID
    
    });
 
  });
  this.headerList = tableHeader;
//}

  this.immutableFormattedData = JSON.parse(JSON.stringify(formattedData));
  this.formattedData = formattedData;
  this.loading = false;

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

}

