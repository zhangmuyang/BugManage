$(function() {
	'use strict';
	var mySdk = {
		ImagesList: [],
		invoke: function(obj) {
			var settings = {
				type: "post",
				url: "",
				data: '',
				success: function(data) {},
				error: function(data) {}
			};
			$.extend(settings, obj);
			$.ajax(settings);
		},
		isNil: function(s) {
			return undefined == s || null == s || $.trim(s) == "" || $.trim(s) == "null";
		},
		selectNumber: function(arr, value) {
			for (var i = 0; i < arr.length; i++) {
				if (arr[i] == value) {
					return i;
				}
			};
		},
		selectText: function(arr, num) {
			for (var i = 0; i < arr.length; i++) {
				if (i == num) {
					return arr[i];
				}
			};
		},
		//日期格式处理
		Dateformat: function(datetime) {
			var newdate; //要转换的时间
			if (datetime != null) {
				var arys = datetime.split('T');
				newdate = arys[0];
			}
			return newdate;
		},
		readFile: function(obj) {
			var file = obj.files[0];

			//判断类型是不是图片  
			if (!/image\/\w+/.test(file.type)) {
				$.alert("请确保文件为图像类型");
				return false;
			}
			var reader = new FileReader();
			reader.readAsDataURL(file);
			reader.onload = function(e) {
				// alert(this.result);
				var ImagesList = mySdk.ImagesList;
				ImagesList.push(this.result);
				mySdk.ImagesList = ImagesList;
				var html = '<li class="ranks"><div class="ranks-media" style="background-image:url(' + this.result + ')"><span class="ranks-remove"></span></div></li>'
				$('.file-inp').before(html); //就是base64  
			}
		},
		getBugDetail: function(id) {
			// console.log(id);
			// location.href="bugDetail.html";
			$.each(mySdk.bugData, function(i, v) {

			})

		}

	}

	//对话框
	$(document).on("pageInit", "#page-bugDetail", function(e, id, page) {
		var $content = $(page).find('.content');
		$content.on('click', '.deleteBug', function() {
			$.confirm('你确定吗?', function() {
				$.alert('你点击了确定按钮!');
			});
		});

	});
	$(document).on('click', '#page-detail-back', function() {
		$('.page').removeClass('page-current');
		$('#page-bug').addClass('page-current');
	})
	$(document).on('click', '#page-bug ul li', function() {
			var id = $(this).attr('data-action');
			// mySdk.getBugDetail(id)
			$.each(mySdk.bugData, function(i, v) {
					if (id == v.FID) {
						mySdk.currentData = v;
						$('.page').removeClass('page-current');
						$('#page-detail').addClass('page-current');
						var osArr = ['IOS', 'Android', '其他'];
						var emArr = ['一般', '紧急', '严重'];
						var bugArr = ['appBug', '服务器Bug', '其他'];
						$('#detail-name').val(v.FCreateName);
						$('#versionNum').val(v.FVersion);
						$('#detail-bug-name').val(v.FBugName);
						$('#phone-platform').val(mySdk.selectText(osArr, v.FOS));
						$('#phone-type').val(v.FMobile);
						$('#text-bug-momo').val(v.FMemo);
						$('#detail-bug-type').val(mySdk.selectText(bugArr, v.FBugType));
						$('#detail-bug-emergency').val(mySdk.selectText(emArr, v.FBugLevel));
						if (v.FSTATUS == '1' || v.FSTATUS == 1) {
							$('#text-bug-status').val('开启');
							$('#li-bug-closename').hide();
							$('#li-bug-closememo').hide();
							$('#li-bug-editdate').hide();
						} else {
							$('#text-bug-status').val('关闭');
							$('#li-bug-closename').show();
							$('#li-bug-closememo').show();
							$('#li-bug-editdate').show();

						}

						$('#text-bug-closename').val(v.FCloseName);
						$('#text-bug-closememo').val(v.FCloseMemo);
						$('#text-bug-indate').val(mySdk.Dateformat(v.FINDATE));
						$('#text-bug-editdate').val(mySdk.Dateformat(v.FEditDATE));




						return;
					};
				})
				// location.href="bugDetail.html";

			// console.log(mySdk.bugData)
			// $.each(mySdk.bugData,function(i,v){
			// //   if (id==v.FID) {
			// //     alert('fahfhafhah')
			// $('#phone-platform').val(v.FCreateName);
			// console.log($('#phone-platform').val())
			// //     // $('#versionNum').val(v.FCreateName);
			// //     // return;  
			// //   };
			// })
		})
		// picker
	$(document).on("pageInit", "#page-bugAdd", function(e, id, page) {
		$("#add-os-platform").picker({
			toolbarTemplate: '<header class="bar bar-nav">\
      <button class="button button-link pull-right close-picker">确定</button>\
      </header>',
			cols: [{
				textAlign: 'center',
				values: ['IOS', 'Android', '其他'],
			}]
		});
		$("#add-bug-type").picker({
			toolbarTemplate: '<header class="bar bar-nav">\
      <button class="button button-link pull-right close-picker">确定</button>\
      </header>',
			cols: [{
				textAlign: 'center',
				values: ['appBug', '服务器Bug', '其他'],
			}]
		});
		$("#add-bug-emergency").picker({
			toolbarTemplate: '<header class="bar bar-nav">\
      <button class="button button-link pull-right close-picker">确定</button>\
      </header>',
			cols: [{
				textAlign: 'center',
				values: ['一般', '紧急', '严重']
			}]
		});
	});

	// 添加
	$(document).on("click", "#add-success", function() {
		var name = $('#add-create-name').val();
		var bugname = $('#add-detail-name').val();
		var verNum = $('#add-versionNum').val();
		var platform = $('#add-os-platform').val();
		var type = $('#add-phone-type').val();
		var bugtype = $('#add-bug-type').val();
		var emergency = $('#add-bug-emergency').val();
		var text = $('#bug-text-con').val();
		var osArr = ['IOS', 'Android', '其他'];
		var emArr = ['一般', '紧急', '严重'];
		var bugArr = ['appBug', '服务器Bug', '其他'];

		if (mySdk.isNil(name)) {
			return $.alert('请添加姓名');
		};
		if (mySdk.isNil(bugname)) {
			return $.alert('请添加bug名称');
		};
		if (mySdk.isNil(verNum)) {
			return $.alert('请添加版本号');
		};
		if (mySdk.isNil(platform)) {
			return $.alert('请选择手机系统类型');
		};
		if (mySdk.isNil(type)) {
			return $.alert('请选择手机类型');
		};
		if (mySdk.isNil(bugtype)) {
			return $.alert('请选择bug类型');
		};
		if (mySdk.isNil(emergency)) {
			return $.alert('请选择紧急程度');
		};
		if (mySdk.isNil(text)) {
			return $.alert('请添加bug内容');
		};
		mySdk.invoke({
			type: "post",
			url: "http://123.57.73.171:8066/api/BugManage/AddBug",
			data: {
				FBugName: bugname,
				FVersion: verNum,
				FOS: mySdk.selectNumber(osArr, platform),
				FMobile: type,
				FOSVersion: '5.1',
				FBugLevel: mySdk.selectNumber(emArr, emergency),
				FBugType: mySdk.selectNumber(bugArr, bugtype),
				FMemo: text,
				FImageList: mySdk.ImagesList,
				FCreateName: name
			},
			success: function(result) {
				if (result.code == '1') {
					$.alert('提交成功', function() {

						location.href = 'bug.html';
					})
				};

			},
			error: function(result) {
				$.alert('提交失败，请稍后重试！')
			},
		})
	});
	// 添加图片
	$(document).on("change", "#file-input-btn", function() {

			// var imgUrl=$('#file-input-btn').val();
			var _this = $('#file-input-btn')[0];
			// console.log(_this.files[0]);
			mySdk.readFile(_this);
		})
		// 删除图片
	$(document).on("click", ".ranks span", function() {
		var ranksLi = $(this).closest('li');
		var index = $(this).index();
		var ImagesList = mySdk.ImagesList;
		mySdk.ImagesList = ImagesList.splice(index, 1);
		ranksLi.remove();
	})
	$(document).on('pageInit', '#page-bug', function() {
		mySdk.invoke({
			type: "post",
			url: "http://123.57.73.171:8066/api/BugManage/BugList",
			success: function(result) {
				if (result.code == '1') {
					var data = result.result_data;
					mySdk.bugData = data;
					var html = "";
					$.each(data, function(i, v) {
						html += '<li data-action="' + v.FID + '"><a href="#"  class="item-link item-content"><div class="item-inner" >' + '<div class="item-title-row"><div class="item-title">创建人：' + v.FCreateName + '</div>' + '<div class="item-after">版本号:' + v.FVersion + '</div></div><div class="item-subtitle">手机型号：' + v.FMobile + '</div>' + '<div class="item-text">bug内容：' + v.FMemo + '</div></div></a></li>'
					})
					$('#page-bug ul').append(html);
				}
			},
			error: function(result) {
				$.alert('提交失败，请稍后重试！')
			}
		})
	})
	$.init();
});