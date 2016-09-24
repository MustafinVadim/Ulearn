﻿$(document).ready(function () {
	var $exerciseScoreForm = $('.exercise__score-form');
	var $scoreBlock = $('.exercise__score');
	var $otherScoreLink = $scoreBlock.find('a');
	var $otherScoreInput = $scoreBlock.find('[name=exerciseScore]');

	$scoreBlock.on('click', 'a', function (e) {
		e.preventDefault();
		$scoreBlock.find('.btn-group .btn').removeClass('active');
		$otherScoreInput.show();
		$otherScoreInput.focus();
		$otherScoreLink.addClass('active');
	});

	$scoreBlock.find('.btn-group').on('click', '.btn', function () {
		var wasActive = $(this).hasClass('active');
		$scoreBlock.find('.btn-group .btn').removeClass('active');
		$(this).toggleClass('active', !wasActive);

		$otherScoreInput.hide();
		$otherScoreLink.removeClass('active');
		$otherScoreInput.val(wasActive ? "" : $(this).data('value'));
	});

	$exerciseScoreForm.find('input[type=submit]').click(function() {
		if ($otherScoreInput.is(':invalid')) {
			$otherScoreInput.show();
			$otherScoreLink.addClass('active');
		}
	});

	$('.exercise__add-review').each(function () {
		var $topReviewComments = $('.exercise__top-review-comments');
		if ($topReviewComments.find('.comment').length == 0) {
			$(this).addClass('without-comments');
			return;
		}
		var $topComments = $topReviewComments.clone(true).removeClass('hidden');
		$('.exercise__add-review__top-comments').append($topComments);
	});

	$('.exercise__top-review-comments .comment a').click(function(e) {
		e.preventDefault();
		$('.exercise__add-review__comment')
			.val($(this).data('value'))
			.trigger('input');
	});
});